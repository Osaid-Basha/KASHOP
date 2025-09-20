using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet("/status/{status}")]
        public async Task<IActionResult> GetOrderByStatus(OrderStatus status)
        {
            var order=await orderService.GetByStatusAsync(status);
            return Ok(order);
        }
        [HttpPatch("/change-status/{orderId}")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, [FromBody] OrderStatus orderStatus)
        {
            var result= await orderService.ChangeStatusAsync(orderId, orderStatus);
            return Ok(new { massage = "Status is Changed" });
        }
    }
}
