using AutoMapper;
using HotelManagement.DTOs.ReservationServices;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;

namespace HotelManagement.Services
{
    public class ResSerService : IResSer
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public ResSerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetResSer>> GetResSer()
        {
            var resSer = await _unitOfWork.GenericRepository<ReservationService>()
                .GetAllAsunc();

            if (!resSer.Any())
                throw new ArgumentNullException("ReservationService is empty..!");

            return _mapper.Map<IEnumerable<GetResSer>>(resSer);
        }

        public async Task PutResSer(PutResSer _resSer, PutResSer resSer)
        {
            if (resSer == null || _resSer == null) throw new ArgumentNullException();


            var current = await _unitOfWork.GenericRepository<ReservationService>()
            .GetByIdAsync(p => p.ReservationId == _resSer.ReservationId && p.ServiceId == _resSer.ServiceId);

            if (current != null)
            {
                current.ReservationId = resSer.ReservationId;
                current.ServiceId = resSer.ServiceId;
                await _unitOfWork.SaveChangesAsync();
            }
            else
                throw new ArgumentNullException();
        }

        public async Task PostResSer(PostResSer resSer)
        {
            if (resSer == null) throw new ArgumentException();

            await _unitOfWork
                .GenericRepository<ReservationService>()
                .PostAsync(_mapper.Map<ReservationService>(resSer));

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteResSer(DeleteResSer resSer)
        {
            var current = await _unitOfWork.GenericRepository<ReservationService>()
            .GetByIdAsync(p => p.ReservationId == resSer.ReservationId && p.ServiceId == resSer.ServiceId);

            if (current != null)
            {
                _unitOfWork.GenericRepository<ReservationService>()
                .Delete(current);

                await _unitOfWork.SaveChangesAsync();
            }
            else throw new ArgumentNullException();
        }
    }
}
