using HotelManagement.DTOs.AppUser;
using HotelManagement.Models;
using HotelManagement.Options;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HotelManagement.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtOption _jwtOption;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(UserManager<AppUser> userManager, JwtOption jwtOption, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtOption = jwtOption;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> GenerateAccessToken(AppUser user)
        {
            var claim = new List<Claim> {
                    new (ClaimTypes.NameIdentifier,user.UserName),
                    new (ClaimTypes.Name,user.FullName),
                    new (ClaimTypes.Email,user.Email)
                };


            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
                claim.Add(new(ClaimTypes.Role, role));

            var tokenHundler = new JwtSecurityTokenHandler();
            var token = new JwtSecurityToken(
                issuer: _jwtOption.Issuer,
                audience: _jwtOption.Audience,
                claims: claim,
                expires: DateTime.UtcNow.AddMinutes(_jwtOption.AccessTokenMinutes),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.SigningKey)),
                    SecurityAlgorithms.HmacSha256)
                );
            return tokenHundler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(string userId)
        {
            var randomBytes = new byte[64];
            RandomNumberGenerator.Create()
                .GetBytes(randomBytes);
            var token = Convert.ToBase64String(randomBytes);
            var activeRefreshToken = new RefreshToken
            {
                Token = token,
                appUserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtOption.RefreshTokenDays)
            };
            return activeRefreshToken;
        }


        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _unitOfWork.TokenRepo.GetToken(refreshToken);
            if (storedToken == null)
                throw new UnauthorizedAccessException("Invalid Refresh Token");
            if (!storedToken.IsActive)
                throw new UnauthorizedAccessException("Refresh Token is expired or revoked.");

            var accessToken =
                  await GenerateAccessToken(storedToken.AppUser);


            await Revoked(storedToken.Token);

            var newRefreshToken = GenerateRefreshToken(storedToken.appUserId);

            if (newRefreshToken != null)
            {
                await _unitOfWork
                    .GenericRepository<RefreshToken>()
                    .PostAsync(newRefreshToken);

                await _unitOfWork.SaveChangesAsync();

            }

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token,
                AccessTokenExpiration =
                    DateTime.UtcNow.AddMinutes(_jwtOption.AccessTokenMinutes)
            };
        }

        public async Task Revoked(string refreshToken)
        {
            var storedToken = await _unitOfWork.TokenRepo.GetToken(refreshToken);
            if (storedToken == null)
                throw new UnauthorizedAccessException("Invalid Refresh Token");
            if (!storedToken.IsActive)
                throw new UnauthorizedAccessException("Refresh Token is expired or revoked.");

            storedToken.RevokedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
