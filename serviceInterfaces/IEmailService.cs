using HotelManagement.Models;

namespace HotelManagement.serviceInterfaces
{
    public interface IEmailService
    {
        Task EmailConfirmation(AppUser user);

        Task SendEmailAsync(string email, string subject, string body);
    }
}
