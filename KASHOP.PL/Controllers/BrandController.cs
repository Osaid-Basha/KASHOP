using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandServices BrandServices;

        public BrandController(IBrandServices BrandServices)
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
