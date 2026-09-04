using HotelManagement.Models;

namespace HotelManagement.Repositories.IRepository
{
    public interface ITokenRepo
    {
        Task<RefreshToken?> GetToken(string refreshToken);
    }
}
