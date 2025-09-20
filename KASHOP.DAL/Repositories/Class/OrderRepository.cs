using KASHOP.DAL.Data;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Class
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Order?> addAsync(Order order)
        {
            await context.orders.AddAsync(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetUserByOrderAsync(int orderId)
        {
            return await context.orders.Include(o => o.User).FirstOrDefaultAsync(O => O.Id == orderId);


        }
        public async Task<List<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await context.orders.Where(o => o.status == status).OrderByDescending(o => o.OrderDate).ToListAsync();
        }
        public async Task<List<Order>> GetAllWithUserAsync(string userId)
        {
            return await context.orders.Where(o => o.UserId == userId).ToListAsync();
        }
        public async Task<List<Order>> GetOederByUserAsync(string userId)
        {
            return await context.orders.Include(o=>o.User).OrderByDescending(o=>o.OrderDate).ToListAsync();
        }
        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order=await context.orders.FindAsync(orderId);
            if (order == null) return false;
            order.status = newStatus;
            var result=await context.SaveChangesAsync();
            return result >0;
        }
    }
 }
