using System.IO;
using Fox.Common.Configurations;
using Fox.Common.Configurations.RabbitMQ;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Logger;
using Fox.Common.Providers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Consumer.CatalogServices;
using RabbitMQ.Consumer.Interfaces;

namespace RabbitMQ.Consumer
{
    public class Program
    {
        private static IConfiguration _configuration = null;
        public static IConfigurationRepository ConfigurationRepository;

        public static void Main(string[] args)
        {
            var appSettingsConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = appSettingsConfiguration.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var catalogConsumer = serviceProvider.GetService<ICatalogConsumerService>();
            catalogConsumer.ProductRevisionConsumer();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            //Get Environment Configurations
            var environment = _configuration.GetSection("MongoConnection:Environment").Value;
            var configurationDocument = ConfigurationRepository.ReadConfiguration(environment).Result;

            //From Configuration Collection take only Product Configs
            var rabbitConfiguration = configurationDocument.GetAs<ConfigurationDocument>("Product");

            var rabbitConnectionString = rabbitConfiguration.GetAs<string>("ConnectionString");
            var rabbitDatabase = rabbitConfiguration.GetAs<string>("Database");

            services.Configure<MongoSettings>(
                options =>
                {
                    options.ConnectionString = rabbitConnectionString;
                    options.Database = rabbitDatabase;
                });

            var elasticSearchIndex = rabbitConfiguration.GetAs<string>("ElasticSearchIndex");
            var elasticSearchProvider = new ElasticSearchProvider(configurationDocument, elasticSearchIndex);
            var logger = new Logger(elasticSearchProvider);

            services.AddSingleton<ILogger>(logger);

            services.RegisterRabbitMqServices();
            services.AddSingleton<IRabbitMqService, RabbitMqService>();

            services.Configure<RabbitMqSettings>(
                options =>
                {
                    options.HostName = "localhost";
                    options.Username = "guest";
                    options.Password = "guest";
                });


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
