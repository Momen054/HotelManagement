using AutoMapper;
using HotelManagement.DTOs.Invoice;
using HotelManagement.DTOs.Payment;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Services
{
    public class InvoiceService : I_Invoice
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IPayment _payment;


        public InvoiceService(IMapper mapper, IUnitOfWork unitOfWork, IPayment payment)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _payment = payment;
        }

        public async Task<IEnumerable<GetInvoice>> GetInvoice()
        {
            var invoice = await _unitOfWork.GenericRepository<Invoice>()
                .GetAllAsunc(p => p.Isdeleted == false);

            if (!invoice.Any())
                throw new ArgumentNullException("Invoice is empty..!");

            return _mapper.Map<IEnumerable<GetInvoice>>(invoice);
        }


        public async Task<GetInvoice> GetInvoice(int id)
        {
            var invoice = await _unitOfWork.GenericRepository<Invoice>()
                .GetByIdAsync(p => p.Isdeleted == false && p.Id == id) ??
                throw new ArgumentNullException("Invoice is empty..!");

            return _mapper.Map<GetInvoice>(invoice);
        }

        public async Task<IEnumerable<GetInvoice>> GetGuestInvoice(string userId)
        {
            var getReservation = await _unitOfWork.GenericRepository<Reservation>()
                .GetAllAsunc(p=>p.AppUserId == userId && p.Isdeleted == false);

            var userInvoices = new List<GetInvoice>();
            foreach (var reservation in getReservation)
            {
                var invoice = await _unitOfWork.GenericRepository<Invoice>()
                .GetByIdAsync(p => p.Isdeleted == false && p.ReservationId == reservation.Id);

                if (invoice != null)
                {
                    userInvoices.Add(_mapper.Map<GetInvoice>(invoice));
                }

            }


            return userInvoices;
        }
        public async Task PutInvoice(PutInvoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException();

            var isExist = _unitOfWork.GenericRepository<Invoice>()
               .GetByIdAsync(p => p.Id == invoice.Id)
               ?? throw new ArgumentNullException(nameof(invoice));

            _unitOfWork.GenericRepository<Invoice>().Put(_mapper.Map<Invoice>(invoice));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateInvoice(Invoice invoice)
        {
            var subTotal = await GetSubTotal(invoice.Id);
            var tax = invoice.Tax;
            decimal total =
               ((tax / 100) * subTotal) + subTotal;


            invoice.SubTotal = subTotal;
            invoice.Total = total;

            _unitOfWork
                .GenericRepository<Invoice>()
                .Put(invoice);

            await _unitOfWork.SaveChangesAsync();

        }
        
        public async Task PostInvoice(PostInvoice invoice)
        {

            Invoice inv = new Invoice
            {
                Isdeleted = false,
                SubTotal = 0,
                Tax = invoice.Tax,
                Total = 0,
                ReservationId = invoice.ReservationId,
            };

            await _unitOfWork
                .GenericRepository<Invoice>()
                .PostAsync(inv);

            await _unitOfWork.SaveChangesAsync();

            await UpdateInvoice(inv);

        }

        public async Task DeleteInvoice(int id)
        {
            var invoice = await _unitOfWork.GenericRepository<Invoice>()
                .GetByIdAsync(p => p.Id == id) ??
                     throw new ArgumentException();
            invoice.Isdeleted = true;
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<decimal> GetSubTotal(int invoiceId)
        {
            var priceOfRooms = await _unitOfWork.InvoiceRepo.GetPriceOfRoom(invoiceId);

            var priceOfService = await _unitOfWork.InvoiceRepo.GetPriceOfService(invoiceId);

            var subTotal = priceOfRooms + priceOfRooms;

            return subTotal;
        }
    }
}
