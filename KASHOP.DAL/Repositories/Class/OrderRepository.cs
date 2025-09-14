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

        public OrderRepository(ApplicationDbContext  context)
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
    }
}
