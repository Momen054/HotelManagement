using HotelManagement.Data;

namespace HotelManagement.Validators.Room
{
    public class GetRoomValidator : PutRoomValidator
    {
        public GetRoomValidator(HotelManagementContext context) : base(context)
        {
        }
    }
}
