using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Catalog.Infrastructure.Interfaces;
using Fox.Catalog.Models;
using Fox.Catalog.Models.ViewModels.Review;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fox.Catalog.Infrastructure.Repositories
{
    public class ReviewRepository : BaseMongoRepository, IReviewRepository
    {
        private readonly IMongoContext _ctx;
        public ReviewRepository(IMongoContext ctx) : base(ctx)
        {
            this._ctx = ctx;
        }
        private IMongoQueryable<Reviews> GetReviewQuery(GetReviewRequest filter)
        {
            var query = _ctx.GetCollection<Reviews>()
                .AsQueryable()
                .BaseFilter();

            query = query.OrderByDescending(x => x.CreatedAt);

            if (!string.IsNullOrEmpty(filter.Id))
            {
                query = query.Where(x => x.Id.Equals(filter.Id));
            }


            if (!string.IsNullOrEmpty(filter.CatalogId))
            {
                query = query.Where(x => x.CatalogId.Equals(filter.CatalogId));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == filter.IsActive);
            }

            if (filter.Take.HasValue)
            {
                query = query.Take(filter.Take.Value);
            }

            if (filter.Skip.HasValue)
            {
                query = query.Skip(filter.Skip.Value);
            }

            return query;
        }

        public async Task<List<Reviews>> GetReviews(GetReviewRequest filter)
        {
            var query = GetReviewQuery(filter);
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<Reviews> Save(Reviews model)
        {
            var isCreate = string.IsNullOrEmpty(model.Id);

            if (isCreate)
            {
                await _ctx.GetCollection<Reviews>().InsertOneAsync(model);
            }
            else
            {
                await _ctx.GetCollection<Reviews>().ReplaceOneAsync(r => r.Id == model.Id, model);
            }

            return model;
        }
    }
}
