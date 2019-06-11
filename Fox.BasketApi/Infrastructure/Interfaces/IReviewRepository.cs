using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.BasketApi.Models;
using Fox.BasketApi.Models.ViewModels.Review;

namespace Fox.BasketApi.Infrastructure.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Reviews>> GetReviews(GetReviewRequest filter);
        Task<Reviews> Save(Reviews model);
    }
}
