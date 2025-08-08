using KASHOP.BLL.Services.Class;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices CategoryServices;

        public CategoriesController(ICategoryServices CategoryServices)
        {
            this.CategoryServices = CategoryServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(CategoryServices.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var category = CategoryServices.GetById(id);
            if (category == null) { return NotFound(); }
            return Ok(category);
        }
    }
}
