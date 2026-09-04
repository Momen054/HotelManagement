using HotelManagement.Data;

namespace HotelManagement.Validators.ReservationServices
{
    public class GetResSerValidator : PutResSerValidator
    {
        public GetResSerValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
