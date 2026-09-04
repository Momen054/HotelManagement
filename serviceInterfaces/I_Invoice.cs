using HotelManagement.DTOs.Invoice;
using HotelManagement.Models;

namespace HotelManagement.serviceInterfaces
{
    public interface I_Invoice
    {
        Task<IEnumerable<GetInvoice>> GetInvoice();

        Task<GetInvoice> GetInvoice(int id);

        Task<IEnumerable<GetInvoice>> GetGuestInvoice(string userId);

        Task PutInvoice(PutInvoice invoice);

        Task PostInvoice(PostInvoice invoice);

        Task UpdateInvoice(Invoice invoice);

        Task DeleteInvoice(int id);
    }
}
