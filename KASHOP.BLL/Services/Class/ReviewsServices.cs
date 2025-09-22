using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.Data.Migrations;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Review = KASHOP.DAL.Model.Review;
namespace KASHOP.BLL.Services.Class
{
    public class ReviewsServices : IReviewsServices
    {
        private readonly IOrderRepository orderRepository;
        private readonly IReviewsRepository reviewsRepository;

        public ReviewsServices(IOrderRepository orderRepository,IReviewsRepository reviewsRepository)
        {
            this.orderRepository = orderRepository;
            this.reviewsRepository = reviewsRepository;
        }
        public async Task<bool> addReviewAsync(ReviewRequest request, string userId)
        {
            var hasOrder=await orderRepository.UserHasApprovedOrderForProductAsync(userId,request.ProductId);

            if (!hasOrder) return false;

            var alreadyReviews=await reviewsRepository.HasUserReviewdProductAsync(userId,request.ProductId);
            if (alreadyReviews) return false;

            var review = reviewsRepository.Adapt<Review>();
            await reviewsRepository.AddReviewAsync(review,userId);
            return true;
        }
    }
}
