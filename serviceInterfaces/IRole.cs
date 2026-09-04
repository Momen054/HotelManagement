using HotelManagement.DTOs.Role;

namespace HotelManagement.serviceInterfaces
{
    public interface IRole
    {
        Task<IEnumerable<GetRole>> GetAll();

        Task<GetRole> GetById(string userId);

        Task PutRole(PutRole Role);


        Task PostRole(PostRole Role);

        Task DeleteRole(DeleteRole role);
    }
}
