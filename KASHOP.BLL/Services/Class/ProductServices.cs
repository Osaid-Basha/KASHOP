using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Interfaces;
using KASHOP.DAL.Repositortrs;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Class
{
    public class ProductServices : GenericServices<ProductRequest, ProductResponses, Product>, IProductServices
    {
        private readonly IProductRepository productRepository;
        private readonly IFileServices fileServices;

        public ProductServices(IProductRepository ProductRepository ,IFileServices fileServices) : base(ProductRepository)
        {
            productRepository = ProductRepository;
            this.fileServices = fileServices;
        }
        public async Task<int> CreateFile(ProductRequest request)
        {
            var entity = request.Adapt<Product>();
            entity.CreatedAt = DateTime.UtcNow;
            if (request.MainImage != null) { 

              var imagePath=  await fileServices.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;

            }
            return productRepository.Add(entity);
        }
    }
}
