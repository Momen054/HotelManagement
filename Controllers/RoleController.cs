using HotelManagement.DTOs.Review;
using HotelManagement.DTOs.Role;
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
    public class RoleController : ControllerBase
    {
        private readonly IRole _role;
        public RoleController(IRole role)
        {
            _role = role;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRole>>> GetRole()
            => Ok(await _role.GetAll());

        // GET: api/Role/5
        [HttpGet("Id")]
        public async Task<ActionResult<GetRole>> GetRole(string Id)
            => Ok(await _role.GetById(Id));


        [HttpPut("")]
        public async Task<IActionResult> PutReview(PutRole role)
        {
            await _role.PutRole(role);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> PostRole(PostRole role)
        {
            await _role.PostRole(role);
            return Ok();
        }

        [HttpDelete("")]
        public async Task<IActionResult> DeleteRole(DeleteRole role)
        {
            await _role.DeleteRole(role);
            return Ok();
        }
    }
}
