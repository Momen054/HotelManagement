using HotelManagement.DTOs.AppUser;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelManagement.Services
{
    public class GoogleLoginService : IGoogleService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _token;
        private readonly IUnitOfWork _unitOfWork;

        public GoogleLoginService(UserManager<AppUser> userManager, ITokenService token,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _token = token;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponseDto> GoogleResponse(AuthenticateResult result)
        {
   
            var email =
                result.Principal.FindFirstValue(ClaimTypes.Email);

            var name =
                result.Principal.FindFirstValue(ClaimTypes.Name);

            var loginProvider =
                 GoogleDefaults.AuthenticationScheme;

            var providerKey =
                result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new AppUser
                {
                    FullName = name,
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user);

                await _userManager.AddLoginAsync(
                    user,
                    new UserLoginInfo
                    (
                        loginProvider,
                        providerKey,
                        loginProvider
                    )
                );

            }


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
    }
}
