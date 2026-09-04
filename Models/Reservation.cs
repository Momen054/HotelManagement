namespace HotelManagement.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int No_Nights { get; set; }

        public string? Status { get; set; }

        public bool? Isdeleted { get; set; }

        public List<ReservationRoom> ReservationRooms { get; set; } = new List<ReservationRoom>();

        public Invoice? Invoices { get; set; } 

        public string AppUserId { get; set; }

        public AppUser? AppUser { get; set; }

        public List<ReservationService> ReservationServices { get; set; } = new List<ReservationService>();

        public Review? Review { get; set; }


    }
}
