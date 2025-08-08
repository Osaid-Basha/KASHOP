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
    public class BrandServices : GenericServices<BrandRequest, BrandResponses, Brand>, IBrandServices
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileServices fileServices;

        public BrandServices(IBrandRepository brandRepository , IFileServices fileServices) : base(brandRepository)
        {
            _brandRepository = brandRepository;
            this.fileServices = fileServices;
        }
        public async Task<int> CreateFile(BrandRequest request)
        {
            var entity = request.Adapt<Brand>();
            entity.CreatedAt = DateTime.UtcNow;
            if (request.MainImage != null)
            {

                var imagePath = await fileServices.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;

            }
            return _brandRepository.Add(entity);
        }
    }
}
