using Fox.Common.Configurations.RabbitMQ;
using Fox.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Consumer.CatalogServices;
using RabbitMQ.Consumer.Interfaces;

namespace RabbitMQ.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var catalogConsumer = serviceProvider.GetService<ICatalogConsumerService>();
            catalogConsumer.ProductRevisionConsumer();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.RegisterRabbitMqServices();
            services.AddSingleton<ICatalogConsumerService, CatalogConsumerService>();
            services.Configure<RabbitMqSettings>(
                options =>
                {
                    options.HostName = "localhost";
                    options.Username = "guest";
                    options.Password = "guest";
                });
        }
    }
}
