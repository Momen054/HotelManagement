using HotelManagement.DTOs.Room;

namespace HotelManagement.serviceInterfaces
{
    public interface IRoom
    {
        Task<IEnumerable<GetRoom>> GetRoom();

        Task<GetRoom> GetRoom(int id);

        Task PutRoom(PutRoom room);

        Task PostRoom(PostRoom room);

        Task DeleteRoom(int id);
    }
}
