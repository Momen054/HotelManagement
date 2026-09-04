using HotelManagement.Data;
using HotelManagement.DTOs.Reservation;
using HotelManagement.Models;
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
    public class ReservationController(IReservation _reservation, IHttpContextAccessor _context) : ControllerBase
    {

        string userId = _context.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        // GET: api/Reservation
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<GetReservation>>> GetReservation()
            => Ok(await _reservation.GetReservation());


        // GET: api/Reservation/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetReservation>> GetReservation(int id)
            => Ok(await _reservation.GetReservation(id));

        [HttpGet("GuestReservation")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<IEnumerable<GetReservation>>> GetGuestReservation()
            => Ok(await _reservation.GetGuestReservation(userId));

        // PUT: api/Reservation
        [HttpPut("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutReservation(PutReservation reservation)
        {
            await _reservation.PutReservation(reservation);
            return Ok();
        }

        [HttpPut("GuestReservation")]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> PutGuestReservation(PutReservation reservation)
        {
            await _reservation.PutGuestReservation(reservation, userId);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Guest,Admin")]
        public async Task<ActionResult<Reservation>> PostReservation(PostReservation reservation)
        {
            await _reservation.PostReservation(reservation);
            return CreatedAtAction("GetReservation", reservation);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _reservation.DeleteReservation(id);
            return Ok();
        }

        [HttpDelete("GuestReservation")]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> DeleteGuestReservation(int id)
        {
            await _reservation.DeleteGuestReservation(id, userId);
            return Ok();
        }
    }
}
