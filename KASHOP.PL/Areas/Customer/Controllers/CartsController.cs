using Azure.Core;
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
    public class CartsController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public CartsController(ICartServices cartServices)
        {
            this._cartServices = cartServices;
        }
        [HttpPost("")]
        public async Task< IActionResult> AddToCart( CartRequest request)
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result=_cartServices.AddToCartAsync(request, userId);
            return await result ? Ok():BadRequest();
        }
        [HttpGet("")]
        public async Task< IActionResult> GetUserCart()
        {
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartServices.CartSummaryResponsesAsync(userId);
            return Ok(result);
        }
    }
}
