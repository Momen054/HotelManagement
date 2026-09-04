using AutoMapper;
using HotelManagement.DTOs.Invoice;
using HotelManagement.DTOs.Payment;
using HotelManagement.DTOs.Reservation;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Services
{
    public class PaymentService : IPayment
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;
        private readonly I_Invoice _invoice;

        public PaymentService(IMapper mapper, IUnitOfWork unitOfWork,I_Invoice invoice)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _invoice = invoice;
        }

        public async Task<IEnumerable<GetPayment>> GetPayment()
        {
            var payment = await _unitOfWork.GenericRepository<Payment>()
                .GetAllAsunc(p => p.Isdeleted == false);

            if (!payment.Any())
                throw new ArgumentNullException("Payment is empty..!");

            return _mapper.Map<IEnumerable<GetPayment>>(payment);
        }


        public async Task<GetPayment> GetPayment(int id)
        {
            var payment = await _unitOfWork.GenericRepository<Payment>()
                .GetByIdAsync(p => p.Isdeleted == false && p.Id == id) ??
                throw new ArgumentNullException("Payment is empty..!");

            return _mapper.Map<GetPayment>(payment);
        }

        public async Task<IEnumerable<GetPayment>> GetGuestPayment(string userId)
        {
            var guestInvoices = await _invoice.GetGuestInvoice(userId);
            
            var guestPayment = new List<GetPayment>();
           
            foreach (var gi in guestInvoices)
            {
                var payment = await _unitOfWork.GenericRepository<Payment>()
                .GetByIdAsync(p => p.Isdeleted == false && p.InvoiceId == gi.Id);

                if (payment != null)
                {
                    guestPayment.Add(_mapper.Map<GetPayment>(payment));
                }

            }

            return guestPayment;
        }

        public async Task PutPayment(PutPayment payment)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));

            var isExist = _unitOfWork.GenericRepository<Payment>()
               .GetByIdAsync(p => p.Id == payment.Id)
               ?? throw new ArgumentNullException(nameof(payment));


            _unitOfWork.GenericRepository<Payment>().Put(_mapper.Map<Payment>(payment));
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task PostPayment(PostPayment payment)
        {
            if (payment == null) throw new ArgumentException(nameof(payment));

            var amount = await _unitOfWork.PaymentRepo.GetAmount(payment.InvoiceId);

            Payment pay = new Payment
            {
                Isdeleted = false,
                Amount = amount,
                Status = payment.Status,
                Method = payment.Method,
                InvoiceId = payment.InvoiceId,
            };

            await _unitOfWork
                .GenericRepository<Payment>()
                .PostAsync(pay);

            await _unitOfWork.SaveChangesAsync();

        }


        public async Task DeletePayment(int id)
        {
            var payment = await _unitOfWork.GenericRepository<Payment>()
                .GetByIdAsync(p => p.Id == id) ??
                     throw new ArgumentException();
            payment.Isdeleted = true;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
