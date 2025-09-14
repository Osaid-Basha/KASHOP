using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetUserByOrderAsync(int id);
        Task<Order?> addAsync(Order order);
    }
}
