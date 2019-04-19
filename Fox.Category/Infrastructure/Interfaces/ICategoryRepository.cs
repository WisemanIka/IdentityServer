using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Category.Models;
using Fox.Category.Models.ViewModels.Category;

namespace Fox.Category.Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Categories>> GetCategories(GetCategoryRequest filter);
        Task<Categories> Save(Categories model);
    }
}
