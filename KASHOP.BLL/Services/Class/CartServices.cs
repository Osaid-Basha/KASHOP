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
    public class CartServices : ICartServices
    {
        private readonly ICartRepository _cartRepository;

        public CartServices(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }

        public async Task< bool> AddToCartAsync(CartRequest Request, string UserId)
        {
            var NewCart = new Cart
            {
                ProductId = Request.ProductId,
                UserId = UserId,
                Count = 1
            };
            return await _cartRepository.AddAsync(NewCart) > 0;
        }

        public async Task<CartSummaryResponses> CartSummaryResponsesAsync(string UserId)
        {
        
            var cartItem = await _cartRepository.getUserCartAsync(UserId) ;


            var response = new CartSummaryResponses
            {
                Item = cartItem.Select(ci => new CartResponses
                {
                  ProductId= ci.ProductId,
                  ProductName= ci.Product.Name ,
                  Price= ci.Product.Price ,
                  Count=ci.Count ,
                }).ToList()
            };

            return response;
        }

        public async Task<bool> ClearCartAsync(string UserId)
        {
            return await _cartRepository.ClearCartAsync(UserId);
        }
    }
}
