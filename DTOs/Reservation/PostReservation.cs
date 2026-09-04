namespace HotelManagement.DTOs.Reservation
{
    public class PostReservation
    {
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int No_Nights { get; set; }

        public string? Status { get; set; }

        public int GuestId { get; set; }
    }
}
