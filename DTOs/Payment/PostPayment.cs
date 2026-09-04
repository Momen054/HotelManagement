namespace HotelManagement.DTOs.Payment
{
    public class PostPayment
    {

        public string? Method { get; set; }

        public string? Status { get; set; }

        public int? InvoiceId { get; set; }

    }
}
