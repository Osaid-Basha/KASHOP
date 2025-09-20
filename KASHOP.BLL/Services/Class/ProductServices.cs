using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Interfaces;
using KASHOP.DAL.Repositortrs;
using Mapster;
using Microsoft.AspNetCore.Http;
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

        public ProductServices(IProductRepository ProductRepository, IFileServices fileServices) : base(ProductRepository)
        {
            productRepository = ProductRepository;
            this.fileServices = fileServices;
        }
        public async Task<int> CreateProduct(ProductRequest request)
        {
            var entity = request.Adapt<Product>();
            entity.CreatedAt = DateTime.UtcNow;
            if (request.MainImage != null)
            {

                var imagePath = await fileServices.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;

            }
            if (request.SubImage != null)
            {

                var SubImagePath = await fileServices.UploadManyAsync(request.SubImage);
                entity.SubImages = SubImagePath.Select(img => new ProductImage { Name = img }).ToList();
            }
            return productRepository.Add(entity);
        }

        public async Task<List<ProductResponses>> GetAllProducts(HttpRequest request, bool onlayActive = false)
        {
            var product = productRepository.GetAllProductWithImage();
            if (onlayActive)
            {

                product = product.Where(p => p.Status == Status.Active).ToList();
            }

            return product.Select(p => new ProductResponses
            {
                Id = p.Id,
                Name = p.Name,
                MainImage =$"{request.Scheme}://{request.Host}/{p.MainImage}",
                SubImageUrls = p.SubImages.Select(img => $"{request.Scheme}://{request.Host}/{p.Name}").ToList(),
            }).ToList();
        }
    }
}
