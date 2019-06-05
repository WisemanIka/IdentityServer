using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Provider.Infrastructure.Interfaces;
using Fox.Provider.Models;
using Fox.Provider.Models.ViewModels.Provider;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fox.Provider.Infrastructure.Repositories
{
    public class ProviderRepository : BaseMongoRepository, IProviderRepository
    {
        private readonly IMongoContext _ctx;

        public ProviderRepository(IMongoContext ctx) : base(ctx)
        {
            this._ctx = ctx;
        }

        private IMongoQueryable<Providers> GetProviderQuery(GetProviderRequest filter)
        {
            var query = _ctx.GetCollection<Providers>()
                .AsQueryable()
                .BaseFilter();

            if (!string.IsNullOrEmpty(filter.Id))
            {
                query = query.Where(x => x.Id == filter.Id);
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

            return query;
        }

        public async Task<List<Providers>> GetProviders(GetProviderRequest filter)
        {
            var query = GetProviderQuery(filter);
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<Providers> Save(Providers model)
        {
            var isCreate = string.IsNullOrEmpty(model.Id);

            if (isCreate)
            {
                await _ctx.GetCollection<Providers>().InsertOneAsync(model);
            }
            else
            {
                await _ctx.GetCollection<Providers>().ReplaceOneAsync(r => r.Id == model.Id, model);
            }

            return model;
        }
    }
}
