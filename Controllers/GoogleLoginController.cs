using HotelManagement.serviceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleLoginController : ControllerBase
    {
        private readonly IGoogleService _service;

        public GoogleLoginController(IGoogleService service) 
        {
            _service = service;
        }
        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(GoogleResponse))
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            AuthenticateResult result =
                await HttpContext.AuthenticateAsync(
                    GoogleDefaults.AuthenticationScheme);
           
            if (!result.Succeeded)
                return BadRequest(new
                {
                    Error = result.Failure?.Message,
                    Properties = result.Properties?.Items
                });

            var token = _service.GoogleResponse(result);

            return Ok(new
            {
                Token = token
            });
        }
    }
}
