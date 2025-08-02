using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Class
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationServices(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<UserResponses> LoginAsync(LoginRequest loginRequest)
        {
            var user=await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
            {
                throw new Exception("Invalid email or password");
            }

           var isPassValid= await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isPassValid)
            {
                throw new Exception("Invalid email or password");
            }
            


                return new UserResponses()
                {
                    Email = loginRequest.Email,
                };
            
            
        }

        public async Task<UserResponses> RegisterAsync(RegisterRequest registerRequest)
        {
            var user = new ApplicationUser()
            {
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                UserName = registerRequest.UserName,
                PhoneNumber = registerRequest.PhoneNumber,
            };
          var Result=  await _userManager.CreateAsync(user,registerRequest.Password);

            if (Result.Succeeded)
            {
                return new UserResponses()
                {
                    Email = registerRequest.Email,
                };

            }
            else
            {
                throw new Exception($"{Result.Errors}");
               
            }

            }
    }
}
