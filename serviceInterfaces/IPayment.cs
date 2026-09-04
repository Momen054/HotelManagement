using HotelManagement.DTOs.Payment;

namespace HotelManagement.serviceInterfaces
{
    public interface IPayment
    {
        Task<IEnumerable<GetPayment>> GetPayment();

        Task<GetPayment> GetPayment(int id);

        Task<IEnumerable<GetPayment>> GetGuestPayment(string userId);

        Task PutPayment(PutPayment payment);

        Task PostPayment(PostPayment payment);

        Task DeletePayment(int id);
    }
}
