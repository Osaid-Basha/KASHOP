using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetUserByOrderAsync(int id);
        Task<Order?> addOrderAsync(Order order);
        Task<List<Order>> GetAllWithUserAsync(string userId);
        Task<List<Order>> GetByStatusAsync(OrderStatus status);
        Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus);
        Task<List<Order>> GetOederByUserAsync(string userId);
    }
}
