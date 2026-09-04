using HotelManagement.Repositories.IRepository;

namespace HotelManagement.UnitOfWork
{
    public interface IUnitOfWork
    {

        public I_InvoiceRepo InvoiceRepo { get; }

        public IPaymentRepo PaymentRepo { get; }

        public ITokenRepo TokenRepo { get; }

        public IGenericRepository<T> GenericRepository<T>() where T : class;

        Task<int> SaveChangesAsync();

    }
}
