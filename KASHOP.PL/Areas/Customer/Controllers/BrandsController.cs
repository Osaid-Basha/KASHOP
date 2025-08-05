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
    public class BrandsController : ControllerBase
    {
        private readonly IBrandServices BrandServices;

        public BrandsController(IBrandServices BrandServices)
        {
            this.BrandServices = BrandServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(BrandServices.GetAll(true));
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var brand = BrandServices.GetById(id);
            if (brand == null) { return NotFound(); }
            return Ok(brand);
        }
    }
}
