using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Class
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
       public AuthenticationServices(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this._userManager = userManager;
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
            var token=await CreateTokenAsync(user);


                return new UserResponses()
                {
                    Token = token,
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
                    Token = registerRequest.Email,
                };

            }
            else
            {
                throw new Exception($"{Result.Errors}");
               
            }

            } 

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),


            };
            var Roles =await _userManager.GetRolesAsync(user);
            foreach (var role in Roles) {

                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwtOptios")["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: Claims,
                expires: DateTime.Now.AddDays(15),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
