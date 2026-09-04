using HotelManagement.DTOs.Payment;

namespace HotelManagement.DTOs.Invoice
{
    public class PostInvoice
    {

        public decimal Tax { get; set; }

        public int ReservationId { get; set; }

    }
}
