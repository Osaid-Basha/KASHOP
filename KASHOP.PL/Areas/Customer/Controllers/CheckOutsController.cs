using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KASHOP.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CheckOutsController : ControllerBase
    {
        private readonly ICheckOutServices checkOutServices;

        public CheckOutsController(ICheckOutServices checkOutServices)
        {
            this.checkOutServices = checkOutServices;
        }
        [HttpPost("payment")]
        public async Task<IActionResult> Payment([FromBody] CheckOutRequest request)
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response=await checkOutServices.ProcessPaymentAsync(request, userId,Request);
            return Ok(response);

        }
        [HttpGet("Success/{orderId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Success([FromRoute] int orderId)
        {
            var result= await checkOutServices.HandlePaymentSuccessAsync(orderId);
            return Ok("Success");
        }
        [HttpGet("Cancel")]
        [AllowAnonymous]
        public IActionResult Cancel()
        {
            return Ok("Cancel");
        }
    }
}
