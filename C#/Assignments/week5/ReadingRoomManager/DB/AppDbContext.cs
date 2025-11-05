using Microsoft.EntityFrameworkCore;
using ReadingRoomManager.Entities;

namespace ReadingRoomManager.DB
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        // set of entities for table to object mapping
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasKey(r => r.Id);

            modelBuilder.Entity<Room>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure Reservation
            modelBuilder.Entity<Reservation>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.room)
                .WithMany(rm => rm.Reservations)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index for faster conflict/time-range queries
            modelBuilder.Entity<Reservation>()
                .HasIndex(r => new { r.RoomId, r.Start, r.End });

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Room A", Capacity = 4 },
                new Room { Id = 2, Name = "Room B", Capacity = 5 },
                new Room { Id = 3, Name = "Room C", Capacity = 2 },
                new Room { Id = 4, Name = "Room D", Capacity = 7 }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    RoomId = 1,
                    Start = new DateTime(2025, 11, 1, 9, 0, 0),
                    End = new DateTime(2025, 11, 1, 11, 0, 0),
                    Status = ReservationStatus.Confirmed
                },
                  new Reservation
                  {
                      Id = 2,
                      RoomId = 2, // FK to Room B
                      Start = new DateTime(2025, 11, 2, 14, 0, 0),
                      End = new DateTime(2025, 11, 2, 16, 0, 0),
                      Status = ReservationStatus.Pending
                  }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
