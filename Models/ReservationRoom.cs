namespace HotelManagement.Models
{
    public class ReservationRoom
    {
        public int RoomId { get; set; }

        public int ReservationId { get; set; }

        public Reservation? Reservation { get; set; }
        
        public Room? Room { get; set; }
    }
}
