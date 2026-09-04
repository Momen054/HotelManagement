using HotelManagement.DTOs.AppUser;
using HotelManagement.Models;
using HotelManagement.Options;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagement.Services
{
    public class AuthenticationService : IAuthentication
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtOption _jwtOption;
        private readonly IEmailService _emailService;
        private readonly ITokenService _token;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(UserManager<AppUser> userManager , JwtOption jwtOption, IEmailService emailService, ITokenService token,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtOption = jwtOption;
            _emailService = emailService;
            _token = token;
            _unitOfWork = unitOfWork;
        }

        public async Task Register(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
            {
               throw new UnauthorizedAccessException();
            }

            var user = new AppUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.UserName,
            };
            await _emailService.EmailConfirmation(user);

            var resulte = await _userManager.CreateAsync(user, dto.Password);

            if (!resulte.Succeeded)
                throw new Exception();

        }

        public async Task<AuthResponseDto> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
               throw new UnauthorizedAccessException( "Invalid Username or Password");

            var valid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!valid)
                throw new UnauthorizedAccessException("Invalid Username or Password");


            var activeRefreshToken = await _unitOfWork.TokenRepo.GetToken(user.Id);
           
            if (activeRefreshToken == null)
            {
                activeRefreshToken = _token.GenerateRefreshToken(user.Id);
            }

            var authDto = await _token.RefreshTokenAsync(activeRefreshToken.Token);

            return new AuthResponseDto
            {
                AccessToken = authDto.AccessToken,
                RefreshToken = authDto.RefreshToken,
                AccessTokenExpiration = authDto.AccessTokenExpiration
            };

        }


        public async Task ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var confirm = await _userManager.ConfirmEmailAsync(user, token);

            if (!confirm.Succeeded)
                throw new UnauthorizedAccessException();
        }
        
        
        public async Task ChangePassword(ChangePasswordDto dto,string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new UnauthorizedAccessException();

            var result = await _userManager.ChangePasswordAsync(
                user,
                dto.CurrentPassword,
                dto.NewPassword);

            if (!result.Succeeded)
            {
                throw new Exception($"{result.Errors}");

            }
 
        }

        public async Task ForgotPassword(ForgotPasswordDto dto)
        {
            var user =
                await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
               throw new UnauthorizedAccessException();

            var token =
                await _userManager.GeneratePasswordResetTokenAsync(user);

            token = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(token));

            var resetLink =
                $"https://localhost:4200/reset-password?email={dto.Email}&token={token}";
            await _emailService.SendEmailAsync(
            dto.Email,
            "Reset Password",
            $"Click here to reset your password <br/><a href='{resetLink}'>Reset Password</a>");
            
        }

        public async Task ResetPassword(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new Exception("Invalid Request");
            }

            dto.Token = Encoding.UTF8.GetString(
                WebEncoders.Base64UrlDecode(dto.Token));

            var result = await _userManager.ResetPasswordAsync(
                user,
                dto.Token,
                dto.NewPassword);

            if (!result.Succeeded)
            {
                throw new Exception("Invalid Request");
            }
        }



    }
}
