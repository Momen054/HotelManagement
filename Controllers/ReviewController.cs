using HotelManagement.Data;
using HotelManagement.DTOs.Review;
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
    [Authorize(Roles = "Guest,Admin")]
    public class ReviewController(IReview _review, IHttpContextAccessor _context) : ControllerBase
    {


        string userId = _context.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        // GET: api/Review
        [HttpGet]
        [Authorize(Roles = "Guest,Admin")]
        public async Task<ActionResult<IEnumerable<GetReview>>> GetReview()
            => Ok(await _review.GetReview());


        // GET: api/Review/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetReview>> GetReview(int id)
            => Ok(await _review.GetReview(id));

        [HttpGet("GuestReview")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<IEnumerable<GetReview>>> GetGuestReview()
            => Ok(await _review.GetGuestReview(userId));

        // PUT: api/Review/5
        [HttpPut("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutReview(PutReview review)
        {
            await _review.PutReview(review);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Guest")]
        public async Task<ActionResult<Review>> PostReview(PostReview review)
        {
            await _review.PostReview(review);
            return CreatedAtAction("GetReview", review);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await _review.DeleteReview(id);
            return Ok();
        }

        [HttpDelete("GuestReview")]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> DeleteGuestReview(int id)
        {
            await _review.DeleteGuestReview(id,userId);
            return Ok();
        }
    }
}
