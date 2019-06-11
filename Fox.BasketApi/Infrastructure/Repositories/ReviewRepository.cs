using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.BasketApi.Infrastructure.Interfaces;
using Fox.BasketApi.Models;
using Fox.BasketApi.Models.ViewModels.Review;

namespace Fox.BasketApi.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public Task<List<Reviews>> GetReviews(GetReviewRequest filter)
        {
            throw new System.NotImplementedException();
        }

        public Task<Reviews> Save(Reviews model)
        {
            throw new System.NotImplementedException();
        }
    }
}
