namespace HotelManagement.DTOs.Review
{
    public class PostReview
    {
        public int GuestId { get; set; }

        public int ReservationId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}
