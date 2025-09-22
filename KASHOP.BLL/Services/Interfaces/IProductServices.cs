using Azure.Core;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IProductServices: IGenericServices<ProductRequest,ProductResponses,Product>
    {
       Task< int> CreateProduct(ProductRequest request);
        Task<List<ProductResponses>> GetAllProducts(HttpRequest request, bool onlayActive = false, int pageNumber = 1, int pageSize = 1);

    }
}
