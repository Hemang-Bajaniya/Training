using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using ReadingRoomManager.Entities.DTOS;

namespace ReadingRoomManager.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        // n:1 relation room
        public Room room { get; set; }
    }

    public static class ReservationExtension
    {
        public static IQueryable<ReservationDto> ToReservationDTO(this IQueryable<Reservation> reservations)
        {
            return reservations.Select(r => new ReservationDto { Id = r.Id, RoomId = r.RoomId, RoomName = r.room.Name, Capacity = r.room.Capacity, Start = r.Start, End = r.End, Status = r.Status });
        }
    }
}
