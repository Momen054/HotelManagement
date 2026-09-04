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
    public class Reservation_Service : IReservation
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly I_Invoice _i_Invoice;


        public Reservation_Service(IMapper mapper, IUnitOfWork unitOfWork, I_Invoice i_Invoice)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _i_Invoice = i_Invoice;
        }

        public async Task<IEnumerable<GetReservation>> GetReservation()
        {
            var reservation = await _unitOfWork.GenericRepository<Reservation>()
                .GetAllAsunc(p => p.Isdeleted == false);

            if (!reservation.Any())
                throw new ArgumentNullException("Reservation is empty..!");

            return _mapper.Map<IEnumerable<GetReservation>>(reservation);
        }


        public async Task<GetReservation> GetReservation(int id)
        {
            var reservation = await _unitOfWork.GenericRepository<Reservation>()
                .GetByIdAsync(p => p.Isdeleted == false && p.Id == id) ??
                throw new ArgumentNullException("Reservation is empty..!");

            return _mapper.Map<GetReservation>(reservation);
        }

        public async Task<IEnumerable<GetReservation>> GetGuestReservation(string id)
        {
            var reservation = await _unitOfWork.GenericRepository<Reservation>()
                .GetAllAsunc(p => p.Isdeleted == false && p.AppUserId == id);

            if (!reservation.Any())
                throw new ArgumentNullException("Reservation is empty..!");

            return _mapper.Map<IEnumerable<GetReservation>>(reservation);
        }

        public async Task PutReservation(PutReservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException();

            var exist = await _unitOfWork.GenericRepository<Reservation>()
              .GetByIdAsync(p => p.Id == reservation.Id)
              ?? throw new ArgumentNullException(nameof(reservation));



            _mapper.Map(reservation,exist);
            await _unitOfWork.SaveChangesAsync();
         
            var invoice = await _unitOfWork.GenericRepository<Invoice>()
                .GetByIdAsync(p => p.ReservationId == reservation.Id)
                    ?? throw new ArgumentNullException();
           
            
            await _i_Invoice.UpdateInvoice(invoice);
        }

        public async Task PutGuestReservation(PutReservation reservation,string id)
        {
            if (reservation == null) throw new ArgumentNullException();

            var exist = await _unitOfWork.GenericRepository<Reservation>()
              .GetByIdAsync(p => p.Id == reservation.Id && p.AppUserId == id)
              ?? throw new ArgumentNullException(nameof(reservation));



            _mapper.Map(reservation, exist);
            await _unitOfWork.SaveChangesAsync();

            var invoice = await _unitOfWork.GenericRepository<Invoice>()
                .GetByIdAsync(p => p.ReservationId == reservation.Id)
                    ?? throw new ArgumentNullException();


            await _i_Invoice.UpdateInvoice(invoice);
        }

        public async Task PostReservation(PostReservation reservation)
        {
            if (reservation == null) throw new ArgumentException();

            await _unitOfWork
                .GenericRepository<Reservation>()
                .PostAsync(_mapper.Map<Reservation>(reservation));

            await _unitOfWork.SaveChangesAsync();

        }


        public async Task DeleteReservation(int id)
        {
            var reservation = await _unitOfWork.GenericRepository<Reservation>()
                .GetByIdAsync(p => p.Id == id) ??
                     throw new ArgumentException();
            reservation.Isdeleted = true;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteGuestReservation(int id,string userId)
        {
            var reservation = await _unitOfWork.GenericRepository<Reservation>()
                .GetByIdAsync(p => p.Id == id && p.AppUserId == userId) ??
                     throw new ArgumentException();
            reservation.Isdeleted = true;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
