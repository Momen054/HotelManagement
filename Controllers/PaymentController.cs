using HotelManagement.Data;
using HotelManagement.DTOs.Payment;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController(IPayment _payment, IHttpContextAccessor _context) : ControllerBase
    {

        string userId = _context.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier).Value;


        // GET: api/Payment
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<GetPayment>>> GetPayment()
            => Ok(await _payment.GetPayment());


        // GET: api/Payment/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetPayment>> GetPayment(int id)
            => Ok(await _payment.GetPayment(id));

        [HttpGet("GuestPayment")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<IEnumerable<GetPayment>>> GetGuestPayment()
        => Ok(await _payment.GetGuestPayment(userId));

        // PUT: api/Payment
        [HttpPut("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutPayment( PutPayment payment)
        {
            await _payment.PutPayment(payment);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Payment>> PostPayment(PostPayment payment)
        {
            await _payment.PostPayment(payment);
            return CreatedAtAction("GetPayment", payment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            await _payment.DeletePayment(id);
            return Ok();
        }
    }
}
