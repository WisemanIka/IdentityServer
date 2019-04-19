using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Category.Models.ViewModels.Category;

namespace Fox.Category.Infrastructure.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAll(GetCategoryRequest filter);
        Task<CategoryResponse> GetById(string id);
        //Task<List<SimpleDictionary>> GetAsDictionary(CategoryFilter filter);
        //Task<ValidationResultModel<CategoryModel>> Save(CategoryModel model);
        //Task<bool> Delete(string Id);
    }
}
