using HotelManagement.DTOs.ReservationRooms;
using HotelManagement.DTOs.ReservationServices;

namespace HotelManagement.serviceInterfaces
{
    public interface IResSer
    {
        Task<IEnumerable<GetResSer>> GetResSer();

        Task PutResSer(PutResSer _resSer, PutResSer resSer);

        Task PostResSer(PostResSer resSer);

        Task DeleteResSer(DeleteResSer resSer);
    }
}
