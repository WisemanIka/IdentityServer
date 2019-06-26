using Fox.Catalog.Infrastructure.Interfaces;
using Fox.Catalog.Infrastructure.Repositories;
using Fox.Catalog.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fox.Catalog.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}
