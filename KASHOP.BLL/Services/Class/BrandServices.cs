using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Interfaces;
using KASHOP.DAL.Repositortrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Class
{
    public class BrandServices : GenericServices<BrandRequest, BrandResponses, Brand>, IBrandServices
    {
        private readonly IBrandRepository brandRepository;

        public BrandServices(IBrandRepository categoryRepository) : base(categoryRepository)
        {
            brandRepository = categoryRepository;
        }
    }
}
