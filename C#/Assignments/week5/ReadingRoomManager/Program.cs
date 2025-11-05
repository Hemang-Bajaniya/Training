
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using ReadingRoomManager.DB;
using ReadingRoomManager.Entities;
using ReadingRoomManager.Entities.DTOS;

namespace ReadingRoomManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // get base dir path
            var basePath = AppContext.BaseDirectory;
            // get data source .db file path from config
            var dbPath = builder.Configuration.GetConnectionString("ReadingRoomDb");
            // store data source .db file at
            var connString = $"Data Source={dbPath}";

            // Add services to the container.
            builder.Services.AddSqlite<AppDbContext>(dbPath);

            // builder.Services.AddTransient<AppDbContext>();
            // builder.Services.AddScoped<AppDbContext>();
            // builder.Services.AddSingleton<AppDbContext>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.ConfigureHttpJsonOptions(o =>
            {
                o.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("fixed", opt =>
            {
                opt.Window = TimeSpan.FromSeconds(10);
                opt.PermitLimit = 5; // 5 req 10 sec
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.QueueLimit = 2;
            });
        });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRateLimiter();



            // root
            app.MapGet("/", () => "Room Reservation Managment!").RequireRateLimiting("fixed");

            // get all rooms
            app.MapGet("/rooms", async (AppDbContext context) =>
            {
                var rooms = await context.Rooms.ToListAsync();
                System.Console.WriteLine(rooms.Count);
                if (rooms.Count > 0)
                {
                    return Results.Ok(rooms);
                }

                return Results.NotFound("No rooms available");
            });

            // get specific room of id
            app.MapGet("/rooms/{id}", async (int id, AppDbContext db) =>
            await db.Rooms.FindAsync(id)
            is Room room
            ? Results.Ok(room)
            : Results.NotFound($"not found room with id {id}"));

            // create a room
            app.MapPost("/rooms", async (Room room, AppDbContext db) =>
        {
            var context = new ValidationContext(room);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(room, context, results, true))
                return Results.ValidationProblem(results.ToDictionary(
                    r => r.MemberNames.FirstOrDefault() ?? "",
                    r => new[] { r.ErrorMessage ?? "" }));

            db.Rooms.Add(room);
            await db.SaveChangesAsync();
            return Results.Created($"/rooms/{room.Id}", room);
        });


            // update room
            app.MapPut("/rooms/{id}", async (int id, Room roomToUpdate, AppDbContext context) =>
            {
                var room = await context.Rooms.FindAsync(id);

                if (room == null)
                    return Results.NotFound("not found room to be update");

                room.Name = roomToUpdate.Name;
                room.Capacity = roomToUpdate.Capacity;

                await context.SaveChangesAsync();

                return Results.NoContent();
            });

            // delete a room
            app.MapDelete("/rooms/{id}", async (int id, AppDbContext context) =>
            {
                var room = await context.Rooms.Include(r => r.Reservations).FirstOrDefaultAsync(r => r.Id == id);

                if (room == null)
                    return Results.NotFound("not found room to be delete");

                var isReserved = await context.Reservations.AnyAsync(r =>
                    r.RoomId == id &&
                    r.End > DateTime.Now &&
                    (r.Status == ReservationStatus.Pending || r.Status == ReservationStatus.Confirmed));

                if (isReserved)
                    return Results.BadRequest("room has reservations");

                context.Rooms.Remove(room);

                await context.SaveChangesAsync();

                return Results.NoContent();
            });

            // resevation routes
            app.MapGet("/reservations", async (int? roomId, DateTime? from, DateTime? to, AppDbContext db) =>
            {
                var query = db.Reservations
                            .Include(r => r.room)
                            .AsQueryable().ToReservationDTO();

                if (roomId.HasValue)
                    query = query.Where(r => r.RoomId == roomId.Value);

                if (from.HasValue && to.HasValue)
                    query = query.Where(r => r.Start >= from && r.End <= to);

                var result = await query.ToListAsync();
                return Results.Ok(result);
            });

            // post reservation
            app.MapPost("/reservations", async (Reservation reservation, AppDbContext db) =>
            {
                if (reservation.Start >= reservation.End)
                    return Results.BadRequest("start date must be before end date");

                var room = await db.Rooms.FindAsync(reservation.RoomId);

                if (room == null)
                    return Results.BadRequest("room not found");

                bool isConflict = await db.Reservations.AnyAsync(r => r.RoomId == reservation.RoomId &&
                r.Status == ReservationStatus.Confirmed &&
                r.Start < reservation.End &&
                r.End > reservation.Start);

                if (isConflict)
                    return Results.BadRequest("room is already reserved");

                db.Reservations.Add(reservation);
                await db.SaveChangesAsync();

                var reservationDto = new ReservationDto { Id = reservation.Id, RoomId = reservation.RoomId, RoomName = room.Name, Capacity = room.Capacity, Start = reservation.Start, End = reservation.End, Status = reservation.Status };

                return Results.Created($"/reservations/{reservationDto.Id}", reservationDto);
            });

            // get reservation by id
            app.MapGet("/reservations/{id}", async (int id, AppDbContext db) =>
            await db.Reservations.FindAsync(id)
            is Reservation reservation
            ? Results.Ok(reservation)
            : Results.NotFound($"not found reservation with id {id}"));

            // update reservation
            app.MapPut("/reservations/{id}", async (int id, Reservation reservationToUpdate, AppDbContext context) =>
            {
                var reservationExists = await context.Reservations.FindAsync(id);

                if (reservationExists == null)
                    return Results.NotFound("not found reservation to be update");

                if (reservationToUpdate.Start >= reservationToUpdate.End)
                    return Results.BadRequest("start date must be before end date");

                var room = await context.Rooms.FindAsync(reservationToUpdate.RoomId);

                if (room == null)
                    return Results.BadRequest("room not found");

                bool isConflict = await context.Reservations.AnyAsync(r => r.RoomId == reservationToUpdate.RoomId &&
                r.Start < reservationToUpdate.End &&
                r.End > reservationToUpdate.Start);

                if (isConflict)
                    return Results.BadRequest("room is already reserved");

                reservationExists.Start = reservationToUpdate.Start;
                reservationExists.End = reservationToUpdate.End;
                reservationExists.Status = reservationToUpdate.Status;
                reservationExists.RoomId = reservationToUpdate.RoomId;

                await context.SaveChangesAsync();

                return Results.NoContent();
            });

            //delete a resevation
            app.MapDelete("/reservations/{id}", async (int id, AppDbContext context) =>
            {
                var reservation = await context.Reservations.FindAsync(id);

                if (reservation == null)
                    return Results.NotFound("not found reservation to be delete");

                context.Reservations.Remove(reservation);

                await context.SaveChangesAsync();

                return Results.NoContent();
            });

            // top reserved rooms
            app.MapGet("/analytics/toprooms", async (AppDbContext db, int count = 5) =>
            {
                var result = await db.Reservations.GroupBy(r => r.RoomId).Select(r => new
                {
                    RoomId = r.Key,
                    Count = r.Count(),
                    RoomName = r.FirstOrDefault().room.Name
                })
                .OrderByDescending(r => r.Count)
                .Take(count)
                .ToListAsync();

                return Results.Ok(result);
            });

            // Conflicting reservations
            app.MapGet("/analytics/conflicts", async (AppDbContext db) =>
            {
                var conflicts = await db.Reservations
                    .Include(r => r.room)
                    .ToListAsync();

                var conflictList = conflicts
                    .SelectMany(r1 => conflicts,
                        (r1, r2) => new { r1, r2 })
                    .Where(x => x.r1.RoomId == x.r2.RoomId
                                && x.r1.Id != x.r2.Id
                                && x.r1.Start < x.r2.End
                                && x.r1.End > x.r2.Start)
                    .Select(x => new
                    {
                        Room = x.r1.room.Name,
                        A = new { x.r1.Id, x.r1.Start, x.r1.End },
                        B = new { x.r2.Id, x.r2.Start, x.r2.End }
                    })
                    .Distinct()
                    .ToList();

                return Results.Ok(conflictList);
            }).WithName("ConflictingReservations");


            // Room utilization (%)
            app.MapGet("/analytics/utilization", async (AppDbContext db) =>
            {
                var rooms = await db.Rooms
                    .Include(r => r.Reservations)
                    .ToListAsync();

                // Assume we measure utilization within 1 day span
                DateTime dayStart = DateTime.Today;
                DateTime dayEnd = dayStart.AddDays(1);
                double totalDayMinutes = (dayEnd - dayStart).TotalMinutes;

                var result = rooms.Select(r =>
                {
                    double reservedMinutes = r.Reservations
                        .Where(res => res.Start >= dayStart && res.End <= dayEnd)
                        .Sum(res => (res.End - res.Start).TotalMinutes);

                    double utilization = Math.Round(reservedMinutes / totalDayMinutes * 100, 2);
                    return new
                    {
                        r.Id,
                        r.Name,
                        UtilizationPercent = utilization
                    };
                }).ToList();

                return Results.Ok(result);
            }).WithName("RoomUtilization");

            app.MapControllers();

            app.MigrateDB();

            app.Run();
        }
    }
}
