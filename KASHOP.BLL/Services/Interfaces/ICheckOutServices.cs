using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface ICheckOutServices
    {
       Task<CheckOutResponses>  ProcessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest HttpRequest);
        Task<bool> HandlePaymentSuccessAsync(int orderId);
    }
}
