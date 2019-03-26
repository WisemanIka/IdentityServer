using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Catalog.Models;
using Fox.Catalog.Models.ViewModels.Product;

namespace Fox.Catalog.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Products>> GetProducts(GetProductRequest filter);
        Task<Products> Save(Products model);
        Task<bool> Delete(string id);
    }
}
