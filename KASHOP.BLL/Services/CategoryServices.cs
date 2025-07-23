using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositortrs;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository CategoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            this.CategoryRepository = categoryRepository;
        }
        public int CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            return CategoryRepository.Add(category);
        }

        public int DeleteCategory(int id)
        {
            var category = CategoryRepository.GetById(id);
            if (category is null) { return 0; }
            return CategoryRepository.Remove(category);
        }

        public IEnumerable<CategoryResponses> GetAllCategories()
        {
            var category = CategoryRepository.GetAll();
            return category.Adapt<IEnumerable<CategoryResponses>>();
        }

        public CategoryResponses? GetCategoryById(int id)
        {
            var category = CategoryRepository.GetById(id);
            return category is null ? null : category.Adapt<CategoryResponses>();
        }

        public int UpdateCategory(int id, CategoryRequest request)
        {
            var category = CategoryRepository.GetById(id);
            if (category is null) { return 0; }
            category.Name = request.Name;
            return CategoryRepository.Update(category);
        }
        public bool ToggleStatus(int id)
        {
            var category = CategoryRepository.GetById(id);
            if (category is null) { return false; }
            category.Status = category.Status == Status.Active ? Status.Active : Status.Inactive;
            CategoryRepository.Update(category);
            return true;
        }

       
    }
}
