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
            return Ok(BrandServices.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var brand = BrandServices.GetById(id);
            if (brand == null) { return NotFound(); }
            return Ok(brand);
        }
        [HttpPost]
        public IActionResult create([FromBody] BrandRequest request)
        {

            var id = BrandServices.Create(request);
            return Ok(new { message = "add succses" });
        }


        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] BrandRequest request)
        {
            var updated = BrandServices.Update(id, request);
            return updated > 0 ? Ok() : NotFound();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {

            var updated = BrandServices.ToggleStatus(id);
            return updated ? Ok(new { message = "status toggled" }) : NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = BrandServices.Delete(id);
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
