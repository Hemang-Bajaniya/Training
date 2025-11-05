//using Xunit;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using ReadingRoomManager.DB;
//using ReadingRoomManager.Entities;
//using Xunit;

//public class AnalyticsServiceTests
//{
//    private AppDbContext GetDbContext()
//    {
//        var options = new DbContextOptionsBuilder<AppDbContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique DB per test
//            .Options;
//        var db = new AppDbContext(options);

//        db.Rooms.Add(new Room { Id = 1, Name = "Room A", Capacity = 10 });
//        db.Rooms.Add(new Room { Id = 2, Name = "Room B", Capacity = 5 });

//        db.Reservations.AddRange(
//            new Reservation
//            {
//                RoomId = 1,
//                Start = DateTime.Today.AddHours(9),
//                End = DateTime.Today.AddHours(11),
//                Status = ReservationStatus.Confirmed
//            },
//            new Reservation
//            {
//                RoomId = 1,
//                Start = DateTime.Today.AddHours(10),
//                End = DateTime.Today.AddHours(12),
//                Status = ReservationStatus.Confirmed
//            },
//            new Reservation
//            {
//                RoomId = 2,
//                Start = DateTime.Today.AddHours(9),
//                End = DateTime.Today.AddHours(10),
//                Status = ReservationStatus.Confirmed
//            }
//        );

//        db.SaveChanges();
//        return db;
//    }

//    [Fact]
//    public async Task FindConflicts_ShouldReturnConflictingReservations()
//    {
//        // Arrange
//        var db = GetDbContext();

//        // Act
//        var conflicts = await db.Reservations
//            .Where(r1 => db.Reservations.Any(r2 =>
//                r1.RoomId == r2.RoomId &&
//                r1.Id != r2.Id &&
//                r1.Start < r2.End &&
//                r1.End > r2.Start))
//            .ToListAsync();

//        // Assert
//        conflicts.Should().HaveCount(2);
//    }

//    [Fact]
//    public async Task GetTopBusiestRooms_ShouldReturnRoomAFirst()
//    {
//        var db = GetDbContext();

//        var result = await db.Reservations
//            .GroupBy(r => r.RoomId)
//            .Select(g => new { RoomId = g.Key, Count = g.Count() })
//            .OrderByDescending(x => x.Count)
//            .FirstAsync();

//        result.RoomId.Should().Be(1);
//        result.Count.Should().Be(2);
//    }
//}
