using System.Diagnostics.CodeAnalysis;

namespace HotelManagement.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }

        public bool? Isdeleted { get; set; }

        public int ReservationId { get; set; }

        public Reservation? Reservation { get; set; }

        public Payment? Payment { get; set; }
        
    }
}
