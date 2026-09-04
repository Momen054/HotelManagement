using HotelManagement.Data;
using HotelManagement.DTOs.Service;
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
    public class ServiceController : ControllerBase
    {
        private readonly IService _service;
        public ServiceController(IService service)
        {
            _service = service;
        }

        // GET: api/Service
        [HttpGet]
        [Authorize(Roles = "Admin,Guest")]
        public async Task<ActionResult<IEnumerable<GetService>>> GetService()
            => Ok(await _service.GetService());


        // GET: api/Service/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetService>> GetService(int id)
            => Ok(await _service.GetService(id));


        // PUT: api/Service
        [HttpPut("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutService(PutService service)
        {
            await _service.PutService(service);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Service>> PostService(PostService service)
        {
            await _service.PostService(service);
            return CreatedAtAction("GetService", service);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteService(int id)
        {
            await _service.DeleteService(id);
            return Ok();
        }
    }
}
