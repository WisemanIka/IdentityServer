using System.Linq;
using Fox.Common.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Fox.Common.Extensions
{
    public static class ServiceCommonExtensions
    {
        public static void RegisterCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationRepository, ConfigurationRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IServiceCollection Remove<T>(this IServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
            if (serviceDescriptor != null) services.Remove(serviceDescriptor);

            return services;
        }
    }
}
