using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Catalog.Models.ViewModels.Product;
using Fox.Common.Responses;

namespace Fox.Catalog.Infrastructure.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAll(GetProductRequest request);
        Task<ProductResponse> GetById(string id);
        Task<ValidationResultModel<ProductResponse>> Save(CreateProductRequest model);
        Task<bool> Delete(string id);
    }
}
