using HotelManagement.Data;

namespace HotelManagement.Validators.Room
{
    public class PutRoomValidator : PostRoomValidator
    {
        public PutRoomValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
