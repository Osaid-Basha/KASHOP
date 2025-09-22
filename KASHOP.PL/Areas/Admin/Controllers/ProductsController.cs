using KASHOP.BLL.Services.Class;
using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices productServices;

        public ProductsController(IProductServices productServices)
        {
            this.productServices = productServices;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] ProductRequest request) {


            var result = await productServices.CreateProduct(request);
            return Ok(result);
        }
        [HttpGet("")]
        public IActionResult GetAll([FromQuery]int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            return Ok(productServices.GetAllProducts(Request,false, pageNumber, pageSize));
        }
    }
}
