using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.BasketApi.Models.ViewModels.Review;
using Fox.Common.Responses;

namespace Fox.BasketApi.Infrastructure.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewResponse>> GetAll(GetReviewRequest request);
        Task<ValidationResultModel<ReviewResponse>> Save(CreateReviewRequest request);
    }
}
