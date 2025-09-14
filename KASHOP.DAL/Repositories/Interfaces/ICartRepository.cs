using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task< int> AddAsync(Cart cart);
        Task<List<Cart>> getUserCartAsync(string userId);
        Task<bool> ClearCartAsync(string userId);
    }
}
