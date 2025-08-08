using KASHOP.DAL.Data;
using KASHOP.DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            
            )
        {
            this._context = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public async Task DataSeedingAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
               await _context.Database.MigrateAsync();
            }
            if (!await _context.categories.AnyAsync())
            {
                await _context.categories.AddRangeAsync(
                    new Category { Name = "Clothes" },
                    new Category { Name = "Mobail" }
                    );
            }
            if (!await _context.brands.AnyAsync()) {

                await _context.brands.AddRangeAsync(
                    new Brand { Name = "Samsung" },
                    new Brand { Name = "Apple" },
                    new Brand { Name = "Nike" }


                    );
            }
            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if(! await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));

            }
            if(! await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    Email = "osaid@gmail.com",
                    FullName = "osaid basha",
                    PhoneNumber = "0597232202",
                    UserName = "obasha",
                    EmailConfirmed = true,
                };
                var user2 = new ApplicationUser()
                {
                    Email = "ameer@gmail.com",
                    FullName = "ameer basha",
                    PhoneNumber = "0597232202",
                    UserName = "abasha",
                    EmailConfirmed = true,
                };
                var user3 = new ApplicationUser()
                {
                    Email = "anas@gmail.com",
                    FullName = "anas basha",
                    PhoneNumber = "0597232202",
                    UserName = "nbasha",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(user1,"Pass@1212");
                await _userManager.CreateAsync(user2,"Pass@1212");
                await _userManager.CreateAsync(user3,"Pass@1212");


                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                await _userManager.AddToRoleAsync(user3, "Customer");

            }
            await _context.SaveChangesAsync();

        }
    }
}
