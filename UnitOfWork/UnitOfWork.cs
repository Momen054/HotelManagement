using HotelManagement.Data;
using HotelManagement.Repositories.IRepository;
using HotelManagement.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        
        private readonly HotelManagementContext _context;

        public I_InvoiceRepo InvoiceRepo { get; }

        public IPaymentRepo PaymentRepo { get; }

        public ITokenRepo TokenRepo { get; }

        public UnitOfWork(HotelManagementContext context)
        {
            _context = context;
            InvoiceRepo = new InvoiceRepo(_context);
            PaymentRepo = new PaymentRepo(_context);
            TokenRepo = new TokenRepo(_context);

        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
