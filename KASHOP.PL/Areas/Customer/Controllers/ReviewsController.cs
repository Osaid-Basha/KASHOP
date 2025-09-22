using KASHOP.BLL.Services.Class;
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
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsServices reviewsServices;

        public ReviewsController(IReviewsServices reviewsServices)
        {
            this.reviewsServices = reviewsServices;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequest request)
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result= reviewsServices.addReviewAsync(request, userId);
            return Ok(result);
        }

    }
}
