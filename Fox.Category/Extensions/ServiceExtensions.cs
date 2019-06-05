using Fox.Category.Infrastructure.Interfaces;
using Fox.Category.Infrastructure.Repositories;
using Fox.Category.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fox.Category.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();


            services.AddScoped<IMenuService, MenuService>();
        }
    }
}
