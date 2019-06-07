using Fox.BasketApi.Infrastructure.Interfaces;
using Fox.BasketApi.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fox.BasketApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketService, BasketService>();
        }
    }
}
