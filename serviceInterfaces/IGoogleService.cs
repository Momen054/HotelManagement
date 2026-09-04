using HotelManagement.DTOs.AppUser;
using Microsoft.AspNetCore.Authentication;

namespace HotelManagement.serviceInterfaces
{
    public interface IGoogleService
    {
        Task<AuthResponseDto> GoogleResponse(AuthenticateResult result);
    }
}
