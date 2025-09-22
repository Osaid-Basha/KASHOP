using KASHOP.DAL.Data;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Class
{
    public class ProductRepositories:GenericRepositorycs<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepositories(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }
        public async Task DecreaseQuantityAsync(List<(int productId, int quantity)> items)
        {
            var productIds = items.Select(i => i.productId).ToList();
            var products= await _context.Products.Where(p=>productIds.Contains(p.Id)).ToListAsync();
            foreach (var product in products) 
            { 
                var item=items.First(i=>i.productId == product.Id);
                if(product.Quantity < item.quantity)
                {
                    throw new Exception("Not enough stock available");
                }
                product.Quantity -= item.quantity;
               
            }
            await _context.SaveChangesAsync();
            
        }
        public  List<Product> GetAllProductWithImage()
        {
            return  _context.Products.Include(p=>p.SubImages).Include(s=>s.Reviews).ThenInclude(r=>r.User).ToList();

        }
    }
}
