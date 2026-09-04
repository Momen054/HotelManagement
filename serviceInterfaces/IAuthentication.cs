using HotelManagement.DTOs.AppUser;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.serviceInterfaces
{
    public interface IAuthentication
    {
        Task Register(RegisterDto dto);

        Task<AuthResponseDto> Login(LoginDto dto);

        Task ConfirmEmail(string userId, string token);

        Task ChangePassword(ChangePasswordDto dto, string email);

        Task ForgotPassword(ForgotPasswordDto dto);

        Task ResetPassword(ResetPasswordDto dto);
    }
}
