using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fox.Catalog.Infrastructure.Interfaces;
using Fox.Catalog.Models;
using Fox.Catalog.Models.ViewModels.Product;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fox.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : BaseMongoRepository, IProductRepository
    {
        private readonly IMongoContext _ctx;

        public ProductRepository(IMongoContext ctx) : base(ctx)
        {
            this._ctx = ctx;
        }

        private IMongoQueryable<Products> GetProductQuery(GetProductRequest filter)
        {
            var query = _ctx.GetCollection<Products>()
                .AsQueryable()
                .BaseFilter();

            query = query.OrderByDescending(x => x.CreatedAt);

            if (!string.IsNullOrEmpty(filter.Id))
            {
                query = query.Where(x => x.Id.Equals(filter.Id));
            }

            if (!string.IsNullOrEmpty(filter.CategoryId))
            {
                query = query.Where(x => x.CategoryId.Contains(filter.CategoryId));
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
                                         || x.Description.Contains(filter.SearchText)
                                         || x.Price.Contains(filter.SearchText)
                                         || x.Discount.Contains(filter.SearchText)
                                         || x.Weight.Contains(filter.SearchText));
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

        public async Task<List<Products>> GetProducts(GetProductRequest filter)
        {
            var query = GetProductQuery(filter);
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<Products> Save(Products model)
        {
            var isCreate = string.IsNullOrEmpty(model.Id);

            if (isCreate)
            {
                await _ctx.GetCollection<Products>().InsertOneAsync(model);
            }
            else
            {
                await _ctx.GetCollection<Products>().ReplaceOneAsync(r => r.Id == model.Id, model);
            }

            return model;
        }

        public async Task<ProductRevisions> GetRevision(string id)
        {
            var revision = _ctx.GetCollection<ProductRevisions>()
                .AsQueryable()
                .Where(x => x.Id == id);

            return await revision.SingleOrDefaultAsync();
        }

        public async Task SaveRevision(ProductRevisions revision)
        {

            await _ctx.GetCollection<ProductRevisions>().InsertOneAsync(revision);
        }

        public async Task<bool> Delete(string id)
        {
            var product = (await GetProducts(new GetProductRequest { Id = id })).Single();

            if (product == null)
                return false;

            //product.IsDeleted = true;
            await Save(product);

            return true;
        }
    }
}