using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Models
{
    public class AppUser : IdentityUser
    {

        public string FullName { get; set; } = string.Empty;    
        
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public Review Review { get; set; }


    }
}
