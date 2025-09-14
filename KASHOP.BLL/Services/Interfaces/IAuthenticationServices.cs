using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<UserResponses> LoginAsync(LoginRequest loginRequest);
        Task<UserResponses> RegisterAsync(RegisterRequest registerRequest,HttpRequest request);
        Task<string> ConfirmEmail(string token, string userId);
        Task<bool> ForgotPassowrd(DAL.DTO.Request.ForgotPasswordRequest request);
        Task<bool> ResetPassword(DAL.DTO.Request.ResetPasswordRequest request);

    }
}
