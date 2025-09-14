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
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext Context) 
        {
            _context = Context;
        }
        public async Task<int> AddAsync(Cart cart)
        {
            await _context.carts.AddAsync(cart);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var item =  _context.carts.Where(c => c.UserId == userId);
            _context.carts.RemoveRange(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Cart>> getUserCartAsync(string userId)
        {
                return await _context.carts.Include(x => x.Product).Where(c=>c.UserId== userId).ToListAsync();
        }
    }
}
