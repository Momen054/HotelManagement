using HotelManagement.Data;
using HotelManagement.DTOs.ReservationRooms;
using HotelManagement.DTOs.ReservationServices;
using HotelManagement.DTOs.Room;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationServiceController : ControllerBase
    {
        private readonly IResSer _ResSer;
        public ReservationServiceController(IResSer ResSer)
        {
            _ResSer = ResSer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetResSer>>> GetResSer()
            => Ok(await _ResSer.GetResSer());


        [HttpPut("")]
        public async Task<IActionResult> PutResSer([FromQuery] PutResSer _resSer, PutResSer resSer)
        {
            await _ResSer.PutResSer(_resSer, resSer);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Room>> PostResSer(PostResSer resSer)
        {
            await _ResSer.PostResSer(resSer);
            return Ok();
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> DeleteResSer(DeleteResSer resSer)
        {
            await _ResSer.DeleteResSer(resSer);
            return Ok();
        }
    }
}
