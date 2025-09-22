using KASHOP.DAL.Data;
using KASHOP.DAL.Data.Migrations;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Review = KASHOP.DAL.Model.Review;

namespace KASHOP.DAL.Repositories.Class
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly ApplicationDbContext context;

        public ReviewsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddReviewAsync(Review request, string userId)
        {

            request.UserId = userId;
            request.ReviewDate = DateTime.Now;
            await context.reviews.AddAsync(request);
            await context.SaveChangesAsync();
        }

        public async Task<bool> HasUserReviewdProductAsync(string userId, int productId)
        {
            return await context.reviews.AnyAsync(r => r.UserId == userId && r.ProductId == productId);
        }
    }
}
