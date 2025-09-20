using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Class
{
    public class UserRepositories:IUserRepositories
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepositories(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string userid)
        {
            return await userManager.FindByIdAsync(userid);
        }
         public async Task<bool> BlockUserAsync( string userId, int days)
        {
            var user =await userManager.FindByIdAsync(userId);
            if (user == null) { 
            return false;
            }
            user.LockoutEnd=DateTime.UtcNow.AddDays(days);
            var result=await userManager.UpdateAsync(user);
            return result.Succeeded;

        }
        public async Task<bool> UnBlockUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.LockoutEnd = null;
            var result = await userManager.UpdateAsync(user);
            return result.Succeeded;

        }
        public async Task<bool> IsBlockedAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
          
            return user.LockoutEnd.HasValue && user.LockoutEnd >DateTime.UtcNow;

        }
        public async Task<bool> ChangeUserRoleAsync(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is null) { return false; }
            var currentRoles = await userManager.GetRolesAsync(user); 
            var removeResult =await userManager.RemoveFromRolesAsync(user, currentRoles);
            var addResult = await userManager.AddToRoleAsync(user, roleName);
            return addResult.Succeeded;

        }
    }
}
