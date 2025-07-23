using KASHOP.BLL.Services;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices categoryServices;

        public CategoriesController(ICategoryServices categoryServices) {
            this.categoryServices = categoryServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(categoryServices.GetAllCategories());
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var category =categoryServices.GetCategoryById(id);
            if (category == null) {return NotFound();}
            return Ok(category);
        }
        [HttpPost]
        public IActionResult create([FromBody] CategoryRequest request) {

            var id = categoryServices.CreateCategory(request);
            return Ok(new { message = "add succses" });
        }
        
        
        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryRequest request)
        {
            var updated=categoryServices.UpdateCategory(id, request);
            return updated>0?Ok():NotFound();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id) { 
        
            var updated=categoryServices.ToggleStatus(id);
            return updated?Ok(new {message="status toggled"}):NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted=categoryServices.DeleteCategory(id);
            return deleted>0?Ok():NotFound();
        }

    }
}
