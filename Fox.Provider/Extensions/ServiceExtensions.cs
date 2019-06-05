using Fox.Provider.Infrastructure.Interfaces;
using Fox.Provider.Infrastructure.Repositories;
using Fox.Provider.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fox.Provider.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
        }
    }
}
