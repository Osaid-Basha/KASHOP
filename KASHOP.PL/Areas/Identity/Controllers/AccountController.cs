using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;


namespace KASHOP.PL.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;

        public AccountController(IAuthenticationServices authenticationServices)
        {
            this._authenticationServices = authenticationServices;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserResponses>>Register(DAL.DTO.Request.RegisterRequest registerRequest)
        {
            var Result =await _authenticationServices.RegisterAsync(registerRequest);
            return Ok(Result);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserResponses>> Login(DAL.DTO.Request.LoginRequest loginRequest) 
        {
            var Result = await _authenticationServices.LoginAsync(loginRequest);
            return Ok(Result);
        }



    }
}
