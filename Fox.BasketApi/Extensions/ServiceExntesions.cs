using Fox.Basket.Infrastructure.Interfaces;
using Fox.Basket.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fox.Basket.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketService, BasketService>();
        }
    }
}
