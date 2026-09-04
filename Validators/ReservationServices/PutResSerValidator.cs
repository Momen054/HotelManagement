using HotelManagement.Data;

namespace HotelManagement.Validators.ReservationServices
{
    public class PutResSerValidator : PostResSerValidator
    {
        public PutResSerValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
