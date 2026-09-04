using HotelManagement.Data;

namespace HotelManagement.Validators.Reservation
{
    public class GetReservationValidator : PutReservationValidator
    {
        public GetReservationValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
