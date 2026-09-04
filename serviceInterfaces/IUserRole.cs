using HotelManagement.DTOs.UserRole;

namespace HotelManagement.serviceInterfaces
{
    public interface IUserRole
    {
        Task<GetUserRole> GetUserRole(string userId);

        Task PostUserRole(PostUserRole userRole);

        Task DeleteUserRole(DeleteUserRole userRole);
    }
}
