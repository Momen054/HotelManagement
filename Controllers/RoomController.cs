using HotelManagement.Data;
using HotelManagement.DTOs.Room;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoom _room;
        public RoomController(IRoom room)
        {
            _room = room;
        }

        // GET: api/Room
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRoom>>> GetRoom()
            => Ok(await _room.GetRoom());


        // GET: api/Room/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetRoom>> GetRoom(int id)
            => Ok(await _room.GetRoom(id));


        // PUT: api/Room
        [HttpPut("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutRoom(PutRoom room)
        {
            await _room.PutRoom(room);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Room>> PostRoom(PostRoom room)
        {
            await _room.PostRoom(room);
            return CreatedAtAction("GetRoom", room);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _room.DeleteRoom(id);
            return Ok();
        }
    }
}
