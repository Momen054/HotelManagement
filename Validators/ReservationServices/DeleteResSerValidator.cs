using HotelManagement.Data;

namespace HotelManagement.Validators.ReservationServices
{
    public class DeleteResSerValidator : PostResSerValidator
    {
        public DeleteResSerValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
