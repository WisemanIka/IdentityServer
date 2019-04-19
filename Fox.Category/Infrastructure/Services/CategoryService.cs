using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fox.Category.Infrastructure.Interfaces;
using Fox.Category.Models;
using Fox.Category.Models.ViewModels.Category;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Models;
using Fox.Common.Responses;

namespace Fox.Category.Infrastructure.Services
{
    public class CategoryService : BaseMongoService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ILogger logger, IMongoContext ctx, ICategoryRepository categoryRepository) : base(logger, ctx)
        {
            this._categoryRepository = categoryRepository;
        }

        private async Task<IEnumerable<CategoryResponse>> GetCategories(GetCategoryRequest filter)
        {
            var categories = await _categoryRepository.GetCategories(filter);
            var result = categories.Map<Categories, CategoryResponse>();
            return result;
        }

        public async Task<List<CategoryResponse>> GetAll(GetCategoryRequest filter)
        {
            var result = (await GetCategories(filter)).ToList();
            return result;
        }

        public async Task<CategoryResponse> GetById(string id)
        {
            var filter = new GetCategoryRequest { Id = id };
            var result = (await GetCategories(filter)).SingleOrDefault();
            return result;
        }

        public async Task<List<SimpleDictionary>> GetAsDictionary(GetCategoryRequest filter)
        {
            var result = (await GetCategories(filter)).ToSimpleDictionary();
            return result;
        }

        public async Task<ValidationResultModel<CategoryResponse>> Save(CreateCategoryRequest model)
        {
            var result = new ValidationResultModel<CategoryResponse>();

            if (result.Succeeded)
            {
                var categoryDbModel = model.Map<CreateCategoryRequest, Categories>();
                categoryDbModel = await _categoryRepository.Save(categoryDbModel);
                result.Model = categoryDbModel.Map<Categories, CategoryResponse>();
            }

            return result;
        }

        public async Task<bool> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var category = (await _categoryRepository.GetCategories(new GetCategoryRequest { Id = id })).SingleOrDefault();

            if (category == null)
                return false;

            category.IsDeleted = true;
            category.CreatedAt = DateTime.UtcNow;

            await _categoryRepository.Save(category);

            return true;
        }
    }
}
