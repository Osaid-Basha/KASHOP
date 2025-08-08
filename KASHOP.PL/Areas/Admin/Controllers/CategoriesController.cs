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
            var brand = CategoryServices.GetById(id);
            if (brand == null) { return NotFound(); }
            return Ok(brand);
        }
        [HttpPost]
        public IActionResult create([FromBody] CategoryRequest request)
        {

            var id = CategoryServices.Create(request);
            return Ok(new { message = "add succses" });
        }


        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryRequest request)
        {
            var updated = CategoryServices.Update(id, request);
            return updated > 0 ? Ok() : NotFound();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {

            var updated = CategoryServices.ToggleStatus(id);
            return updated ? Ok(new { message = "status toggled" }) : NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = CategoryServices.Delete(id);
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
