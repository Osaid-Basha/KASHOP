using KASHOP.DAL.Data;
using KASHOP.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositortrs
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext Context)
        {
            _context = Context;
        }

        public int Add(Category category)
        {
            _context.categories.Add(category);
            return _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll(bool withTraking = false)
        { if(withTraking)
            return _context.categories.ToList();
        return _context.categories.AsTracking().ToList();
        }

        public Category? GetById(int id)
        {
            return _context.categories.Find(id);
        }

        public int Remove(Category category)
        {
           _context.categories.Remove(category);
            return _context.SaveChanges();
        }

        public int Update(Category category)
        {
            _context.categories.Update(category);
            return _context.SaveChanges();
        }
    }
}
