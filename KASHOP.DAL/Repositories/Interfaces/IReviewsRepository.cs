using KASHOP.DAL.Data.Migrations;
using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Review = KASHOP.DAL.Model.Review;

namespace KASHOP.DAL.Repositories.Interfaces
{
    public interface IReviewsRepository
    {
        Task<bool> HasUserReviewdProductAsync(string userId, int productId);
        Task AddReviewAsync(Review request, string userId);

    }
}
