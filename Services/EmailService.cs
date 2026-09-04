using HotelManagement.Models;
using HotelManagement.Options;
using HotelManagement.serviceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace HotelManagement.Services
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailSettingOption _options;

        public EmailService(UserManager<AppUser> userManager,EmailSettingOption options)
        {
            _userManager = userManager;
            _options = options;
        }
        
        public async Task EmailConfirmation(AppUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink =
                $"https://localhost:7081/api/auth/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";
            var body = $@"
                <h3>Welcome {user.FullName}</h3>

                <p>Thank you for registering.</p>

                <p>Please click the button below to confirm your email.</p>

                <a href='{confirmationLink}'
                   style='padding:10px 20px;
                          background:#007bff;
                          color:white;
                          text-decoration:none;
                          border-radius:5px;'>
                Confirm Email
                </a>

                <p>If you didn't create this account, ignore this email.</p>
                ";
           await SendEmailAsync(user.Email, $@"<h1>Hello, {user.UserName}</h1>", body);
        }


        public async Task SendEmailAsync(string email, string subject, string body)
        {

            var mail = new MailMessage();
            mail.From =
               new MailAddress(_options.Email, _options.DisplayName);

            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            using var smtp = new SmtpClient();
            smtp.Host = _options.Host;
            smtp.Port = _options.Port;
            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;
            smtp.Credentials =
                new NetworkCredential(_options.Email, _options.Password);

            await smtp.SendMailAsync(mail);
        }

        
    }
}
