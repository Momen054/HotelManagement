using HotelManagement.DTOs.UserRole;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Services
{
    public class UserRoleService : IUserRole
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleService(UserManager<AppUser> userManager , RoleManager<IdentityRole> role)
        {
            _userManager = userManager;
            _roleManager = role;
        }

        public async Task<GetUserRole> GetUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ??
                throw new ArgumentNullException();
            var roles = await _userManager.GetRolesAsync(user);

            var userRole = new GetUserRole
            {
                Roles = roles,
            };
            return userRole;
        }

        [HttpPost]
        public async Task PostUserRole(PostUserRole userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId) ??
                throw new ArgumentNullException();

            foreach (var role in userRole.Roles)
            {
                 var isExist= await _roleManager.FindByNameAsync(role) ??
                    throw new ArgumentNullException($"{nameof(role)} is not exist");
            }

            await _userManager.AddToRolesAsync(user,userRole.Roles);
        }

        [HttpDelete("")]
        public async Task DeleteUserRole(DeleteUserRole userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId) ??
                throw new ArgumentNullException();
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Any())
                throw new ArgumentException();

            await _userManager.RemoveFromRolesAsync(user, roles);

        }
    }
}
