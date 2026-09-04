using HotelManagement.DTOs.RoomType;

namespace HotelManagement.serviceInterfaces
{
    public interface IRoomType
    {
        Task<IEnumerable<GetRoomType>> GetRoomType();

        Task<GetRoomType> GetRoomType(int id);

        Task PutRoomType(PutRoomType RoomType);

        Task PostRoomType(PostRoomType RoomType);

        Task DeleteRoomType(int id);
    }
}
