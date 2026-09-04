namespace HotelManagement.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public List<ReservationService> ReservationServices { get; set; } = new List<ReservationService>();

    }
}
