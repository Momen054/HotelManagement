namespace HotelManagement.DTOs.Invoice
{
    public class PutInvoice : PostInvoice
    {
        public int Id { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }


    }
}
