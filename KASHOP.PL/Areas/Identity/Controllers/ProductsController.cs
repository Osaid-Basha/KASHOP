using KASHOP.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    
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
            var Product = ProductServices.GetAll();
            return Ok(Product);
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var Product = ProductServices.GetById(id);
            if (Product == null) { return NotFound(); }
            return Ok(Product);
        }
    }
}
