using HotelManagement.DTOs.AppUser;
using HotelManagement.Models;

namespace HotelManagement.serviceInterfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(AppUser user);

        RefreshToken GenerateRefreshToken(string userId);

        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);

        Task Revoked(string refreshToken);
    }
}
