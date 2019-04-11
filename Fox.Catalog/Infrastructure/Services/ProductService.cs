using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fox.Catalog.Infrastructure.Interfaces;
using Fox.Catalog.Models;
using Fox.Catalog.Models.ViewModels.Product;
using Fox.Common.Constants;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Responses;

namespace Fox.Catalog.Infrastructure.Services
{
    public class ProductService : BaseMongoService, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRabbitMqService _rabbitMqService;

        public ProductService(ILogger logger, IMongoContext ctx,
            IProductRepository productRepository, IRabbitMqService rabbitMqService) : base(logger, ctx)
        {
            this._productRepository = productRepository;
            this._rabbitMqService = rabbitMqService;
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

            var productDbModel = model.Map<CreateProductRequest, Products>();

            var isEdit = !string.IsNullOrWhiteSpace(model.Id);
            if (isEdit)
            {
                var product = (await _productRepository.GetProducts(new GetProductRequest { Id = model.Id })).Single();
                //Save Revision with RabbitMQ
                await _rabbitMqService.RabbitMqSender(product, RabbitMqConstants.ProductRevisionQueue);
            }

            productDbModel = await _productRepository.Save(productDbModel);

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
