using HotelManagement.Data;

namespace HotelManagement.Validators.Payment
{
    public class GetPaymentValidator : PutPaymentValidator
    {
        public GetPaymentValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
