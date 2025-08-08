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
    public class ProductRepositories:GenericRepositorycs<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepositories(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }

    }
}
