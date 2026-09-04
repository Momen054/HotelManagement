using HotelManagement.DTOs.ReservationRooms;

namespace HotelManagement.serviceInterfaces
{
    public interface IResRom
    {
        Task<IEnumerable<GetResRom>> GetResRom();

        Task PutResRom(PutResRom _resRom, PutResRom resRom);

        Task PostResRom(PostResRom resRom);

        Task DeleteResRom(DeleteResRom resRom);
    }
}
