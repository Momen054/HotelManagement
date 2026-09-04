using AutoMapper;
using HotelManagement.DTOs.RoomType;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;

namespace HotelManagement.Services
{
    public class RoomTypeService : IRoomType
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public RoomTypeService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetRoomType>> GetRoomType()
        {
            var roomType = await _unitOfWork.GenericRepository<RoomType>()
                .GetAllAsunc();

            if (!roomType.Any())
                throw new ArgumentNullException("RoomType is empty..!");

            return _mapper.Map<IEnumerable<GetRoomType>>(roomType);
        }


        public async Task<GetRoomType> GetRoomType(int id)
        {
            var roomType = await _unitOfWork.GenericRepository<RoomType>()
                .GetByIdAsync(p => p.Id == id) ??
                throw new ArgumentNullException("RoomType is empty..!");

            return _mapper.Map<GetRoomType>(roomType);
        }

        public async Task PutRoomType(PutRoomType roomType)
        {
            if (roomType == null) throw new ArgumentNullException();

            var isExist = _unitOfWork.GenericRepository<RoomType>()
              .GetByIdAsync(p => p.Id == roomType.Id)
              ?? throw new ArgumentNullException(nameof(roomType));

            _unitOfWork.GenericRepository<RoomType>()
                .Put(_mapper.Map<RoomType>(roomType));
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task PostRoomType(PostRoomType roomType)
        {
            if (roomType == null) throw new ArgumentException();

            await _unitOfWork
                .GenericRepository<RoomType>()
                .PostAsync(_mapper.Map<RoomType>(roomType));

            await _unitOfWork.SaveChangesAsync();

        }


        public async Task DeleteRoomType(int id)
        {
            _unitOfWork.GenericRepository<RoomType>()
                .Delete(id);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
