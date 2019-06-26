using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fox.Catalog.Infrastructure.Interfaces;
using Fox.Catalog.Models;
using Fox.Catalog.Models.ViewModels.Review;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Responses;

namespace Fox.Catalog.Infrastructure.Services
{
    public class ReviewService : BaseMongoService, IReviewService
    {
        protected readonly ILogger Logger;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(ILogger logger, IMongoContext ctx, IReviewRepository reviewRepository) : base(logger, ctx)
        {
            this._reviewRepository = reviewRepository;
        }

        private async Task<IEnumerable<ReviewResponse>> GetReviews(GetReviewRequest filter)
        {
            var reviews = await _reviewRepository.GetReviews(filter);
            var result = reviews.Map<Reviews, ReviewResponse>();
            return result;
        }

        public async Task<List<ReviewResponse>> GetAll(GetReviewRequest filter)
        {
            var result = (await GetReviews(filter)).ToList();
            return result;
        }

        public async Task<ValidationResultModel<ReviewResponse>> Save(CreateReviewRequest model)
        {
            var result = new ValidationResultModel<ReviewResponse>();

            if (result.Succeeded)
            {
                var reviewDbModel = model.Map<CreateReviewRequest, Reviews>();
                reviewDbModel = await _reviewRepository.Save(reviewDbModel);
                result.Model = reviewDbModel.Map<Reviews, ReviewResponse>();
            }

            return result;
        }
    }
}
