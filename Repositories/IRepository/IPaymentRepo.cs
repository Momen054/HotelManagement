namespace HotelManagement.Repositories.IRepository
{
    public interface IPaymentRepo
    {
        Task<decimal> GetAmount(int? id);
    }
}
