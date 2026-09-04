using AutoMapper;
using HotelManagement.DTOs.Room;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;

namespace HotelManagement.Services
{
    public class RoomService : IRoom
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetRoom>> GetRoom()
        {
            var room = await _unitOfWork.GenericRepository<Room>()
                .GetAllAsunc();

            if (!room.Any())
                throw new ArgumentNullException("Room is empty..!");

            return _mapper.Map<IEnumerable<GetRoom>>(room);
        }


        public async Task<GetRoom> GetRoom(int id)
        {
            var room = await _unitOfWork.GenericRepository<Room>()
                .GetByIdAsync(p => p.Id == id) ??
                throw new ArgumentNullException("Room is empty..!");

            return _mapper.Map<GetRoom>(room);
        }

        public async Task PutRoom(PutRoom room)
        {
            if (room == null) throw new ArgumentNullException();

            var isExist = _unitOfWork.GenericRepository<Room>()
              .GetByIdAsync(p => p.Id == room.Id)
              ?? throw new ArgumentNullException(nameof(room));

            _unitOfWork.GenericRepository<Room>()
                .Put(_mapper.Map<Room>(room));
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task PostRoom(PostRoom room)
        {
            if (room == null) throw new ArgumentException();

            await _unitOfWork
                .GenericRepository<Room>()
                .PostAsync(_mapper.Map<Room>(room));

            await _unitOfWork.SaveChangesAsync();

        }


        public async Task DeleteRoom(int id)
        {
            _unitOfWork.GenericRepository<Room>()
                .Delete(id);

            await _unitOfWork.SaveChangesAsync();
        }

    }
}
