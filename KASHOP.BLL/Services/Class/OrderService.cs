using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Class
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<Order?> addOrderAsync(Order order)
        {
            return await orderRepository.addAsync(order);
        }

        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus)
        {    
            return await orderRepository.ChangeStatusAsync(orderId, newStatus);
        }

        public async Task<List<Order>> GetAllWithUserAsync(string userId)
        {
            return await orderRepository.GetAllWithUserAsync(userId);
        }

        public Task<List<Order>> GetByStatusAsync(OrderStatus status)
        {
            return orderRepository.GetByStatusAsync(status);
        }

        public async Task<List<Order>> GetOederByUserAsync(string userId)
        {
            return await orderRepository.GetOederByUserAsync(userId);
        }

        public async Task<Order?> GetUserByOrderAsync(int id)
        {
            return await orderRepository.GetUserByOrderAsync(id);
        }
    }
}
