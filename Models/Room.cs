namespace HotelManagement.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public int RoomTypeId { get; set; }

        public short Floor { get; set; }

        public string? Status { get; set; }

        public RoomType? RoomType { get; set; }

        public List<ReservationRoom> ReservationRooms { get; set; } = new List<ReservationRoom>();
    }
}
