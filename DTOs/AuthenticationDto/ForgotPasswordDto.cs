using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DTOs.AppUser
{
    public class ForgotPasswordDto
    {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        
    }
}
