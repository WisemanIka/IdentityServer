using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Category.Infrastructure.Interfaces;
using Fox.Category.Models;
using Fox.Category.Models.ViewModels.Category;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fox.Category.Infrastructure.Repositories
{
    public class CategoryRepository : BaseMongoRepository, ICategoryRepository
    {
        private readonly IMongoContext _ctx;
        public CategoryRepository(IMongoContext ctx) : base(ctx)
        {
            this._ctx = ctx;
        }
        private IMongoQueryable<Categories> GetCategoryQuery(GetCategoryRequest filter)
        {
            var query = _ctx.GetCollection<Categories>()
                .AsQueryable()
                .BaseFilter();

            query = query.OrderByDescending(x => x.CreatedAt);

            if (!string.IsNullOrEmpty(filter.Id))
            {
                query = query.Where(x => x.Id.Equals(filter.Id));
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Name.ToLower() == filter.Name.ToLower());
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == filter.IsActive);
            }

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                query = query.Where(x => x.Name.Contains(filter.SearchText)
                                         || x.Description.Contains(filter.SearchText));
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

        public async Task<List<Categories>> GetCategories(GetCategoryRequest filter)
        {
            var query = GetCategoryQuery(filter);
            var result = await query.ToListAsync();
            return result;
        }
    }
}
