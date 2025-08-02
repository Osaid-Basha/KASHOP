using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
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
        Task<UserResponses> RegisterAsync(RegisterRequest registerRequest);
    }
}
