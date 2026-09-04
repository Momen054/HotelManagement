namespace HotelManagement.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string? Method { get; set; }

        public string? Status { get; set; }

        public bool? Isdeleted { get; set; }

        public Invoice? Invoice { get; set; } 

        public int ? InvoiceId { get; set; }

    }
}
