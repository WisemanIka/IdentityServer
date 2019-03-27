using System.Linq;
using Fox.Common.Configurations;
using Fox.Common.Infrastructure;
using Fox.Common.Providers.EmailSender;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Fox.Common.Extensions
{
    public static class ServiceCommonExtensions
    {
        public static void RegisterCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddSingleton<IConfigurationRepository, ConfigurationRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void RegisterEmailService(this IServiceCollection services, EmailSenderSettings emailSenderSettings)
        {
            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    emailSenderSettings.Host,
                    emailSenderSettings.Port,
                    emailSenderSettings.EnableSSL,
                    emailSenderSettings.UserName,
                    emailSenderSettings.Password
                )
            );
        }

        public static IServiceCollection Remove<T>(this IServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
            if (serviceDescriptor != null) services.Remove(serviceDescriptor);

            return services;
        }
    }
}
