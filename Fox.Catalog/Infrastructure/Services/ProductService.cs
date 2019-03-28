using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fox.Catalog.Infrastructure.Interfaces;
using Fox.Catalog.Models;
using Fox.Catalog.Models.ViewModels.Product;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Responses;

namespace Fox.Catalog.Infrastructure.Services
{
    public class ProductService : BaseMongoService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(ILogger logger, IMongoContext ctx,
            IProductRepository productRepository) : base(logger, ctx)
        {
            this._productRepository = productRepository;
        }

        private async Task<IEnumerable<ProductResponse>> GetProducts(GetProductRequest filter)
        {
            var products = await _productRepository.GetProducts(filter);
            var result = products.Map<Products, ProductResponse>();
            return result;
        }

        public async Task<List<ProductResponse>> GetAll(GetProductRequest filter)
        {
            var result = (await GetProducts(filter)).ToList();
            return result;
        }

        public async Task<ProductResponse> GetById(string id)
        {
            var filter = new GetProductRequest { Id = id };
            var result = (await GetProducts(filter)).SingleOrDefault();
            return result;
        }

        public async Task<ValidationResultModel<ProductResponse>> Save(CreateProductRequest model)
        {
            var result = new ValidationResultModel<ProductResponse>();

            if (!result.Succeeded) return result;

            var isEdit = !string.IsNullOrWhiteSpace(model.Id);

            Products product = null;

            if (isEdit)
            {
                product = (await _productRepository.GetProducts(new GetProductRequest { Id = model.Id })).SingleOrDefault();
            }

            var productDbModel = isEdit && product != null ?
                                model.Map<CreateProductRequest, Products>(product) :
                                model.Map<CreateProductRequest, Products>();

            productDbModel = await _productRepository.Save(productDbModel);


            //Rabbit Call Here
            if (isEdit)
            {
                var revisions = await _productRepository.GetRevision(product?.Id);
                revisions?.Revisions?.Add(product);
                await _productRepository.SaveRevision(revisions);
            }

            result.Model = productDbModel.Map<Products, ProductResponse>();

            return result;
        }

        public async Task<bool> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var result = await _productRepository.Delete(id);
            return result;
        }
    }
}
