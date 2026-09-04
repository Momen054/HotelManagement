using HotelManagement.Data;

namespace HotelManagement.Validators.Reservation
{
    public class PutReservationValidator : PostReservationValidator
    {
        public PutReservationValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
