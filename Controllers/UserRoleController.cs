using HotelManagement.DTOs.UserRole;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRole _UserRole;
        public UserRoleController(IUserRole UserRole)
        {
            _UserRole = UserRole;
        }

        // GET: api/UserRole
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserRole>>> GetUserRole(string userId)
            => Ok(await _UserRole.GetUserRole(userId));


        [HttpPost]
        public async Task<ActionResult> PostUserRole(PostUserRole userRole)
        {
            await _UserRole.PostUserRole(userRole);
            return Ok();
        }

        [HttpDelete("")]
        public async Task<IActionResult> DeleteUserRole(DeleteUserRole userRole)
        {
            await _UserRole.DeleteUserRole(userRole);
            return Ok();
        }
    }
}
