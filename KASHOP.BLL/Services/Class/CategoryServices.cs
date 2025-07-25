using KASHOP.BLL.Services.Interfaces;
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

namespace KASHOP.BLL.Services.Class
{
    public class CategoryServices : GenericServices<CategoryRequest,CategoryResponses,Category>, ICategoryServices
    {
        private readonly ICategoryRepository CategoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository):base(categoryRepository) 
        {
            CategoryRepository = categoryRepository;
        }
      


    }
}
