using AutoMapper;
using HotelManagement.DTOs.Role;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelManagement.Services
{
    public class RoleService : IRole
    {

        private readonly RoleManager<IdentityRole> _role;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<IdentityRole> role, IMapper mapper)
        {
            _role = role;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetRole>> GetAll()
        {
            var roles = _role.Roles.ToList();
            return _mapper.Map<IEnumerable<GetRole>>(roles);
        }


        public async Task<GetRole> GetById(string id)
        {
            var role = await _role.FindByIdAsync(id)??
                throw new ArgumentNullException("Invalid Role..!");

            return _mapper.Map<GetRole>(role);
        }

        public async Task PutRole(PutRole Role)
        {
            if (Role == null) throw new ArgumentException();

            var role = await _role.FindByIdAsync(Role.Id) ??
                throw new ArgumentNullException("Invalid Role..!");

            await _role.UpdateAsync(role);

        }

        public async Task PostRole(PostRole role)
        {
            if (role == null) throw new ArgumentException();

            await _role.CreateAsync(_mapper.Map<IdentityRole>(role));

        }

        public async Task DeleteRole(DeleteRole Role)
        {
            var role = await _role.FindByIdAsync(Role.Id) ??
                throw new ArgumentNullException("Invalid Role..!");

            await _role.DeleteAsync(role);
        }
    }
}
