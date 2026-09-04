using AutoMapper;
using HotelManagement.DTOs.Room;
using HotelManagement.DTOs.User;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Services
{
    public class UserService : IUser
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetUser>> GetAll()
        {
            var users = await _unitOfWork.GenericRepository<AppUser>()
                .GetAllAsunc();

            if (!users.Any())
                throw new ArgumentNullException("User is empty..!");

            return _mapper.Map<IEnumerable<GetUser>>(users);
        }

        public async Task<ActionResult<GetUser>> GetUser(string id)
        {
            var user = await _unitOfWork.GenericRepository<AppUser>()
               .GetByIdAsync(p => p.Id == id) ??
               throw new ArgumentNullException("User is empty..!");

            return _mapper.Map<GetUser>(user);
        }

        public async Task DeleteUser(string id)
        {
            _unitOfWork.GenericRepository<AppUser>()
                .Delete(id);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
