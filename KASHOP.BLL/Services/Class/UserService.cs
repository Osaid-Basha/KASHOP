using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Class
{
    public class UserService : IUserService
    {
        private readonly IUserRepositories userRepositories;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService( IUserRepositories userRepositories,UserManager<ApplicationUser> userManager)
        {
            this.userRepositories = userRepositories;
            this.userManager = userManager;
        }

        public async Task<bool> BlockUserAsync(string userId, int days)
        {
            return await userRepositories.BlockUserAsync(userId,days);
        }

        public Task<bool> ChangeUserRoleAsync(string userId, string roleName)
        {
           return userRepositories.ChangeUserRoleAsync(userId,roleName);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users= await userRepositories.GetAllAsync();
            var userDto =new List<UserDto>();
            foreach (var user in users) { 
                var role = await userManager.GetRolesAsync(user);
                userDto.Add(new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    RoleName = role.FirstOrDefault()
                });
            }
            return userDto;
        }

        public async Task<UserDto> GetByIdAsync(string userid)
        {
            var user = await userRepositories.GetByIdAsync(userid);
            return user.Adapt<UserDto>();
        }

        public async Task<bool> IsBlockedAsync(string userId)
        {
            return await userRepositories.IsBlockedAsync(userId);
        }

        public async Task<bool> UnBlockUserAsync(string userId)
        {
            return await userRepositories.UnBlockUserAsync(userId);
        }
    }
}
