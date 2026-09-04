using HotelManagement.Data;

namespace HotelManagement.Validators.ReservationRooms
{
    public class PutResRomValidator : PostResRomValidator
    {
        public PutResRomValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
