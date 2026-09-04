using HotelManagement.Data;
using HotelManagement.DTOs.RoomType;
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
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomType _roomType;
        public RoomTypeController(IRoomType roomType)
        {
            _roomType = roomType;
        }

        // GET: api/RoomType
        [HttpGet]
        [Authorize(Roles = "Admin,Guest")]
        public async Task<ActionResult<IEnumerable<GetRoomType>>> GetRoomType()
            => Ok(await _roomType.GetRoomType());


        // GET: api/RoomType/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetRoomType>> GetRoomType(int id)
            => Ok(await _roomType.GetRoomType(id));


        // PUT: api/RoomType
        [HttpPut("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutRoomType(PutRoomType roomType)
        {
            await _roomType.PutRoomType(roomType);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RoomType>> PostRoomType(PostRoomType roomType)
        {
            await _roomType.PostRoomType(roomType);
            return CreatedAtAction("GetRoomType", roomType);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            await _roomType.DeleteRoomType(id);
            return Ok();
        }
    }
}
