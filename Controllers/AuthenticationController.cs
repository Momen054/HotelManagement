using HotelManagement.DTOs.AppUser;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _auth;

        public AuthenticationController(IAuthentication auth)
        {
            _auth = auth;
        }
        

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _auth.Register(dto);
            return Ok();
        }
      
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
           return Ok(await _auth.Login(dto));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            await _auth.ConfirmEmail(userId, token);
            return Ok("Email Confirmed Successfully");
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            await _auth.ChangePassword(dto , email);
            return Ok(new
            {
                Message = "Password changed successfully."
            });
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            await _auth.ForgotPassword(dto);
            return Ok();
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            await _auth.ResetPassword(dto);
            return Ok(new
            {
                Message = "Password Reset Successfully"
            });
        }


    }
}
