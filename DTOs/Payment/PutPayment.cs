namespace HotelManagement.DTOs.Payment
{
    public class PutPayment : PostPayment
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }


    }
}
