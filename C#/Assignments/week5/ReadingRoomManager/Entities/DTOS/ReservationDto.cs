using System.Text.Json.Serialization;

namespace ReadingRoomManager.Entities.DTOS
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; } = String.Empty;
        public int Capacity { get; set; }
        public int UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ReservationStatus Status { get; set; }
    }
}
