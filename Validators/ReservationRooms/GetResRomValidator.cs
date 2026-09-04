using HotelManagement.Data;

namespace HotelManagement.Validators.ReservationRooms
{
    public class GetResRomValidator : PutResRomValidator
    {
        public GetResRomValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
