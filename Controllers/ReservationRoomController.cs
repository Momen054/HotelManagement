using HotelManagement.Data;
using HotelManagement.DTOs.ReservationRooms;
using HotelManagement.DTOs.Room;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationRoomController : ControllerBase
    {
        private readonly IResRom _resrom;
        public ReservationRoomController(IResRom resrom)
        {
            _resrom = resrom;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetResRom>>> GetResRom()
            => Ok(await _resrom.GetResRom());


        [HttpPut("")]
        public async Task<IActionResult> PutResRom([FromQuery] PutResRom _resRom, PutResRom resRom)
        {
            await _resrom.PutResRom(_resRom, resRom);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Room>> PostResRom(PostResRom resRom)
        {
            await _resrom.PostResRom(resRom);
            return Ok();
        }

        [HttpDelete("")]
        public async Task<IActionResult> DeleteResRom(DeleteResRom resRom)
        {
            await _resrom.DeleteResRom(resRom);
            return Ok();
        }
    }
}
