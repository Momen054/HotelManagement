using HotelManagement.Data;
using HotelManagement.Models;
using HotelManagement.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repositories.Repository
{
    public class InvoiceRepo:I_InvoiceRepo
    {

        private readonly HotelManagementContext _context;

        private readonly DbSet<Invoice> _dbSet;

        public InvoiceRepo(HotelManagementContext context)
        {
            _context = context;
            _dbSet = _context.Invoices;
        }

        public async Task<decimal> GetPriceOfRoom(int id)
        {
            var invoice = await _dbSet
                .Where(p=>p.Id == id)
                .Include(p => p.Reservation)
                    .ThenInclude(p => p.ReservationRooms)
                        .ThenInclude(p => p.Room)
                            .ThenInclude(p => p.RoomType)
                               .FirstOrDefaultAsync();

            decimal total = 0;
            foreach (var item in invoice.Reservation.ReservationRooms)
            {

                total += (item.Room.RoomType.PricePerNight * invoice.Reservation.No_Nights);
            }
            return total;
            
        }


        public async Task<decimal> GetPriceOfService(int id) 
        {
            var invoices = await _dbSet
                .Where(p=>p.Id == id)
                .Include(p => p.Reservation)
                .ThenInclude(p => p.ReservationServices)
                .ThenInclude(p=>p.Service)
                .FirstOrDefaultAsync();

            decimal total = 0;
            foreach (var item in invoices.Reservation.ReservationServices)
            {
                total += item.Service.UnitPrice * item.Service.Quantity;
             
            }

            return total;
        }



        public async Task<Payment?> GetPayment(int id)
            => _context.Payments.FirstOrDefault(p => p.InvoiceId == id); 
    }
}
