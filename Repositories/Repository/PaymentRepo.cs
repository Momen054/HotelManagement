using HotelManagement.Data;
using HotelManagement.Models;
using HotelManagement.Repositories.IRepository;
using HotelManagement.serviceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repositories.Repository
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly HotelManagementContext _context;

        private readonly DbSet<Payment> _dbSet;

        public PaymentRepo(HotelManagementContext context)
        {
            _context = context;
            _dbSet = _context.Payments;
        }

        public async Task<decimal> GetAmount(int? id)
        {
            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(p=>p.Id == id);
            if (invoice == null) return 0;
            return invoice.Total;
        }
    }
}
