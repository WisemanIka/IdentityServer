using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Catalog.Models;
using Fox.Catalog.Models.ViewModels.Review;

namespace Fox.Catalog.Infrastructure.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Reviews>> GetReviews(GetReviewRequest filter);
        Task<Reviews> Save(Reviews model);
    }
}
