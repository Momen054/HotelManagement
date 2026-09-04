
using HotelManagement.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.serviceInterfaces
{
    public interface IUser
    {
        Task<IEnumerable<GetUser>> GetAll();

        Task<ActionResult<GetUser>> GetUser(string id);

        Task DeleteUser(string id);
    }
}
