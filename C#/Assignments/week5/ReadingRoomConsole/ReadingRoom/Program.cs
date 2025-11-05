using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ReadingRoomManager.DB;
using System.Data;
using Microsoft.Data.Sqlite;

var basePath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory)!.FullName,
    "..", "..", "..", "..", "..", "ReadingRoomManager");

var config = new ConfigurationBuilder()
    .SetBasePath(basePath)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection");

var dbPath = Path.Combine(basePath, "DBRoomReservation.db");
connectionString = $"Data Source={dbPath}";

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite(connectionString)
    .Options;

using var db = new AppDbContext(options);

var roomId = 2;
var start = DateTime.Parse("2025-11-01");
var end = DateTime.Parse("2025-11-05");

var reservations = await db.Reservations
    .FromSqlRaw("SELECT * FROM Reservations WHERE RoomId = @roomId AND Start >= @start AND End <= @end",
        new SqliteParameter("@roomId", roomId),
        new SqliteParameter("@start", start),
        new SqliteParameter("@end", end))
    .ToListAsync();

foreach (var item in reservations)
{
    System.Console.WriteLine($"\nRoomId: {item.RoomId}, Start: {item.Start}, End: {item.End}\n\n");
}

Console.WriteLine("1. Top N Busiest Rooms");
Console.WriteLine("2. Conflicting Reservations");
Console.WriteLine("3. Room Utilization %");
Console.Write("\nSelect an option (1-3): ");
var choice = Console.ReadLine();

switch (choice)
{
    case "1":
        TopNBusiestRooms(db);
        break;
    case "2":
        ConflictingReservations(db);
        break;
    case "3":
        UtilizationPerRoom(db);
        break;
    default:
        Console.WriteLine("Invalid choice.");
        break;
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();

DynamicDemo.Run();



static void TopNBusiestRooms(AppDbContext db)
{
    Console.Write("\nEnter start date (yyyy-MM-dd): ");
    DateTime from = DateTime.Parse(Console.ReadLine()!);
    Console.Write("Enter end date (yyyy-MM-dd): ");
    DateTime to = DateTime.Parse(Console.ReadLine()!);
    Console.Write("Enter N: ");
    int N = int.Parse(Console.ReadLine()!);

    var busiestRooms = db.Reservations
        .Include(r => r.room)
        .Where(r => r.Start >= from && r.End <= to)
        .GroupBy(r => r.room!.Name)
        .Select(g => new { Room = g.Key, Count = g.Count() })
        .OrderByDescending(x => x.Count)
        .Take(N)
        .ToList();

    Console.WriteLine("\nTop Rooms (LINQ):");
    foreach (var item in busiestRooms)
        Console.WriteLine($"{item.Room} - {item.Count} reservations");

    var dt = new DataTable();
    dt.Columns.Add("Room", typeof(string));
    dt.Columns.Add("Count", typeof(int));

    foreach (var item in busiestRooms)
        dt.Rows.Add(item.Room, item.Count);

    Console.WriteLine("\nTop Rooms (DataTable):");
    foreach (DataRow row in dt.Rows)
        Console.WriteLine($"{row["Room"]} - {row["Count"]} reservations");
}


static void ConflictingReservations(AppDbContext db)
{
    Console.WriteLine("\nChecking for conflicts...");

    var reservations = db.Reservations.Include(r => r.room).ToList();

    var conflicts = reservations
        .SelectMany(r1 => reservations, (r1, r2) => new { r1, r2 })
        .Where(x => x.r1.RoomId == x.r2.RoomId && x.r1.Id != x.r2.Id &&
                    x.r1.Start < x.r2.End && x.r1.End > x.r2.Start)
        .Select(x => new
        {
            Room = x.r1.room!.Name,
        })
        .Distinct()
        .ToList();

    if (!conflicts.Any())
        Console.WriteLine("No conflicts found!");
    else
    {
        Console.WriteLine("\nConflicts Found:");
        foreach (var c in conflicts)
            Console.WriteLine($"{c.Room}");
    }

    // DataTable version
    var dt = new DataTable();
    dt.Columns.Add("Room");

    foreach (var c in conflicts)
        dt.Rows.Add(c.Room);

    Console.WriteLine("\n(DataTable view)");
    foreach (DataRow row in dt.Rows)
        Console.WriteLine($"{row["Room"]}");
}


static void UtilizationPerRoom(AppDbContext db)
{
    Console.WriteLine("\nCalculating utilization % per room...");

    var rooms = db.Rooms.Include(r => r.Reservations).ToList();

    var utilization = rooms.Select(r =>
    {
        double totalReservedHours = r.Reservations.Sum(res => (res.End - res.Start).TotalHours);
        double totalAvailableHours = 24 * 30;
        double utilizationPct = (totalReservedHours / totalAvailableHours) * 100;

        return new { r.Name, Utilization = Math.Round(utilizationPct, 2) };
    }).ToList();

    Console.WriteLine("\nUtilization (LINQ List):");
    foreach (var item in utilization)
        Console.WriteLine($"{item.Name}: {item.Utilization}%");

    // DataTable version
    var dt = new DataTable();
    dt.Columns.Add("Room");
    dt.Columns.Add("Utilization %", typeof(double));

    foreach (var u in utilization)
        dt.Rows.Add(u.Name, u.Utilization);

    Console.WriteLine("\n(DataTable view):");
    foreach (DataRow row in dt.Rows)
        Console.WriteLine($"{row["Room"]}: {row["Utilization %"]}%");
}
