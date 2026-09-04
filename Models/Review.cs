namespace HotelManagement.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string AppUserId { get; set; }

        public AppUser? AppUser { get; set; }

        public int ReservationId { get; set; }

        public Reservation? Reservation { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

    }
}
