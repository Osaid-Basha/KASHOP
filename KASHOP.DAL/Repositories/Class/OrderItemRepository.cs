using KASHOP.DAL.Data;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Class
{
    public class OrderItemRepository:IOrderItemRepository
    {
        private readonly ApplicationDbContext context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddRangeAsync(List<OrderItem> item)
        {
            await context.orderItems.AddRangeAsync(item);
            await context.SaveChangesAsync();
          
        }
    }
}
