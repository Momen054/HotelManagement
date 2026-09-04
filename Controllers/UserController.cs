using HotelManagement.DTOs.User;
using HotelManagement.serviceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IUser _User, IHttpContextAccessor _context) : ControllerBase
    {

        string userId = _context.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<GetUser>>> GetUser()
            => Ok(await _User.GetAll());

        // GET: api/User/5
        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetUser>> GetUser(string Id)
            => Ok(await _User.GetUser(Id));

        [HttpGet("Guest")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetUser>> GetGuest()
            => Ok(await _User.GetUser(userId));


        [HttpDelete("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _User.DeleteUser(id);
            return Ok();
        }

        [HttpDelete("Guest")]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> DeleteGuest()
        {
            await _User.DeleteUser(userId);
            return Ok();
        }
    }
}
