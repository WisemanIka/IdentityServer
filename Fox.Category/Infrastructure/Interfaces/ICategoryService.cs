using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Category.Models.ViewModels.Category;
using Fox.Common.Models;
using Fox.Common.Responses;

namespace Fox.Category.Infrastructure.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAll(GetCategoryRequest filter);
        Task<CategoryResponse> GetById(string id);
        Task<List<SimpleDictionary>> GetAsDictionary(GetCategoryRequest filter);
        Task<ValidationResultModel<CategoryResponse>> Save(CreateCategoryRequest model);
        Task<bool> Delete(string id);
    }
}
