using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender _emailSender;

        public AuthenticationServices(UserManager<ApplicationUser> userManager,IConfiguration configuration, IEmailSender emailSender )
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public IConfiguration Configuration { get; }

        public async Task<UserResponses> LoginAsync(DAL.DTO.Request.LoginRequest loginRequest)
        {
            var user=await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
            {
                throw new Exception("Invalid email or password");
            }
            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new Exception("please confirm youe email");
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
        public async Task<string> ConfirmEmail(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {

                throw new Exception("user not found");
            }
            var result=await _userManager.ConfirmEmailAsync(user,token);
            if (result.Succeeded)
            {
                return "email confirmed Succeefuly";
            }
            return "email confirmation faild";
        }


        public async Task<UserResponses> RegisterAsync(DAL.DTO.Request.RegisterRequest registerRequest)
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
               var token= await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken=Uri.EscapeDataString(token);
                var emailUrl = $"https://localhost:7080/api/identity/Account/ConfirmEmail?token={escapeToken}&userId={user.Id}";
                await _emailSender.SendEmailAsync(user.Email, "welcome", $"<h1 class='danger' > Hello welcome {user.UserName}</h1>"+$"<a href='{emailUrl}'>confirm </a>");
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
        public async Task<bool> ForgotPassowrd(DAL.DTO.Request.ForgotPasswordRequest request)
        {
            var user =await _userManager.FindByEmailAsync(request.Email);
            if (user == null) { throw new Exception("user not found"); }

            var random=new Random();
            var code = random.Next(1000, 9999).ToString();
            user.CodeResetPassword = code;
            user.PasswordResetCodeExpiry= DateTime.UtcNow.AddMinutes(15);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(request.Email,"reser password",$"<p>code is {code}");
            return true;
        }
        public async Task<bool> ResetPassword(DAL.DTO.Request.ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null) { throw new Exception("user not found"); }
            if(user.CodeResetPassword !=request.Code)return false;
            if(user.PasswordResetCodeExpiry <DateTime.UtcNow) return false;
            var token=await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (result.Succeeded)
            {
                await _emailSender.SendEmailAsync(request.Email, "change password", "<h1> your password is changed </h1>");
            }
            return true;

        }
       


    }
}
