using HotelManagement.Data;
using HotelManagement.DTOs.Invoice;
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
    public class InvoiceController(I_Invoice _invoice, IHttpContextAccessor _context) : ControllerBase
    {

        string userId = _context.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        // GET: api/Invoice
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<GetInvoice>>> GetInvoice()
            => Ok(await _invoice.GetInvoice());


        // GET: api/Invoice/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetInvoice>> GetInvoice(int id)
            => Ok(await _invoice.GetInvoice(id));

        [HttpGet("GuestInvoice")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<IEnumerable<GetInvoice>>> GetGuestInvoice()
            => Ok(await _invoice.GetGuestInvoice(userId));

        // PUT: api/Invoice
        [HttpPut("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutInvoice(PutInvoice invoice)
        {
            await _invoice.PutInvoice(invoice);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Invoice>> PostInvoice(PostInvoice invoice)
        {
            await _invoice.PostInvoice(invoice);
            return CreatedAtAction("GetInvoice", invoice);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            await _invoice.DeleteInvoice(id);
            return Ok();
        }

    }
}
