using HotelManagement.Data;

namespace HotelManagement.Validators.Payment
{
    public class PutPaymentValidator : PostPaymentValidator
    {
        public PutPaymentValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
