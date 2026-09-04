using HotelManagement.Data;
using HotelManagement.Models;
using HotelManagement.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HotelManagement.Repositories.Repository
{
    public class TokenRepo : ITokenRepo
    {
        private HotelManagementContext _context;

        public TokenRepo(HotelManagementContext context)
        {
            _context = context;
        }
        public async Task<RefreshToken?> GetToken(string refreshToken)
            => await _context.refreshTokens
                        .Include(r => r.AppUser)
                        .Where(r => r.Token == refreshToken)
                        .OrderByDescending(r => r.CreatedAt)
                        .FirstOrDefaultAsync();
    }
}
