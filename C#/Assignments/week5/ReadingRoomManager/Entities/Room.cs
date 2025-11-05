using System.ComponentModel.DataAnnotations;

namespace ReadingRoomManager.Entities
{
    public class Room
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Range(1, 100)]
        public int Capacity { get; set; }

        // relation 1:n reservtion
        public ICollection<Reservation> Reservations { get; set; } = [];
    }
}
