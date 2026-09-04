using HotelManagement.DTOs.Service;

namespace HotelManagement.serviceInterfaces
{
    public interface IService
    {
        Task<IEnumerable<GetService>> GetService();

        Task<GetService> GetService(int id);

        Task PutService(PutService service);

        Task PostService(PostService service);

        Task DeleteService(int id);
    }
}
