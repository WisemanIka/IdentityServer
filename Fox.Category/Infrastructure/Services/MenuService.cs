using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fox.Category.Infrastructure.Interfaces;
using Fox.Category.Models;
using Fox.Category.Models.ViewModels.Category;
using Fox.Category.Models.ViewModels.Menu;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;

namespace Fox.Category.Infrastructure.Services
{
    public class MenuService : BaseMongoService, IMenuService
    {
        private readonly ICategoryRepository _categoryRepository;
        public MenuService(ILogger logger, IMongoContext ctx, ICategoryRepository categoryRepository) : base(logger, ctx)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<List<MenuResponse>> GetAll()
        {
            var filter = new GetCategoryRequest { IsActive = true };
            var menu = await _categoryRepository.GetCategories(filter);

            var result = menu.Where(x => string.IsNullOrEmpty(x.ParentId)).OrderBy(x => x.OrderIndex).Select(x => new MenuResponse
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Name = x.Name,
                OrderIndex = x.OrderIndex,
                Picture = x.Picture,
                Url = x.Url,
                Childrens = menu.Where(c => c.ParentId == x.Id).OrderBy(c => c.OrderIndex).Select(c => new MenuResponse
                {
                    Id = c.Id,
                    ParentId = c.ParentId,
                    Name = c.Name,
                    OrderIndex = c.OrderIndex,
                    Picture = c.Picture,
                    Url = c.Url
                }).ToList()
            }).ToList();

            return result;
        }
    }
}
