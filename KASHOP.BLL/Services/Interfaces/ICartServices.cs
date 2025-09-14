using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface ICartServices
    {
        Task<bool> AddToCartAsync(CartRequest Request,string UserId);
        Task<CartSummaryResponses> CartSummaryResponsesAsync(string UserId);
        Task<bool> ClearCartAsync(string UserId);
    }
}
