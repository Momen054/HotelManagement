using HotelManagement.Models;

namespace HotelManagement.Repositories.IRepository
{
    public interface I_InvoiceRepo
    {
        Task<decimal> GetPriceOfRoom(int id);

        Task<decimal> GetPriceOfService(int id);

    }
}
