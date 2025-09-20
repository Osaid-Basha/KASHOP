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
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllUser()
        {
            var user=await userService.GetAllAsync();
            return Ok(user);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserId([FromRoute] string userId) { 
            var user= await userService.GetByIdAsync(userId);
            return Ok(user);
        }
        [HttpPatch("/block/{userId}")]
        public async Task<IActionResult> BlockUser([FromRoute] string userId, [FromBody] int days)
        {
            var result=await userService.BlockUserAsync(userId, days);
            return Ok(result);
        }
        [HttpPatch("/unblock/{userId}")]
        public async Task<IActionResult> UnBlockUser([FromRoute] string userId)
        {
            var result = await userService.UnBlockUserAsync(userId);
            return Ok(result);
        }
        [HttpPatch("/isblock/{userId}")]
        public async Task<IActionResult> IsBlockUser([FromRoute] string userId)
        {
            var result = await userService.IsBlockedAsync(userId);
            return Ok(result);
        }
        [HttpPatch("/ChangeRole/{userId}")]
        public async Task <IActionResult> ChangeRole([FromRoute] string userId, [FromBody] ChangeRoleRequest request)
        {
            var user=await userService.ChangeUserRoleAsync(userId, request.RoleName);
            return Ok(new {massage="role changed successfully."});
        }
       

    }
}
