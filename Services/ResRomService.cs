using AutoMapper;
using HotelManagement.DTOs.ReservationRooms;
using HotelManagement.DTOs.Room;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;

namespace HotelManagement.Services
{
    public class ResRomService : IResRom
    {

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public ResRomService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetResRom>> GetResRom()
        {
            var resrom = await _unitOfWork.GenericRepository<ReservationRoom>()
                .GetAllAsunc();

            if (!resrom.Any())
                throw new ArgumentNullException("ReservationRoom is empty..!");

            return _mapper.Map<IEnumerable<GetResRom>>(resrom);
        }

        public async Task PutResRom(PutResRom _resRom, PutResRom resRom)
        {
            if (resRom == null || _resRom == null) throw new ArgumentNullException();


            var current=await _unitOfWork.GenericRepository<ReservationRoom>()
            .GetByIdAsync(p => p.ReservationId == _resRom.ReservationId && p.RoomId == _resRom.RoomId);

            if (current != null)
            {
                current.ReservationId = resRom.ReservationId;
                current.RoomId = _resRom.RoomId;
                await _unitOfWork.SaveChangesAsync();
            }
            else
                throw new ArgumentNullException();
        }

        public async Task PostResRom(PostResRom resRom)
        {
            if (resRom == null) throw new ArgumentException();

            await _unitOfWork
                .GenericRepository<ReservationRoom>()
                .PostAsync(_mapper.Map<ReservationRoom>(resRom));

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteResRom(DeleteResRom resRom)
        {
            var current = await _unitOfWork.GenericRepository<ReservationRoom>()
            .GetByIdAsync(p => p.ReservationId == resRom.ReservationId && p.RoomId == resRom.RoomId);

            if (current != null) 
            {
                _unitOfWork.GenericRepository<ReservationRoom>()
                .Delete(current);

                await _unitOfWork.SaveChangesAsync();
            }
            else throw new ArgumentNullException();
        }
    }
}
