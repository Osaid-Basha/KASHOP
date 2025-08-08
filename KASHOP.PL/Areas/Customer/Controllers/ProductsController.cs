using KASHOP.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices ProductServices;

        public ProductsController(IProductServices ProductServices)
        {
            this.ProductServices = ProductServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(ProductServices.GetAll(true));
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var brand = ProductServices.GetById(id);
            if (brand == null) { return NotFound(); }
            return Ok(brand);
        }
    }
}
