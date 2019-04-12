using System.Reflection;
using AutoMapper;
using Fox.Common.Configurations;
using Fox.Common.Configurations.RabbitMQ;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Logger;
using Fox.Common.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Consumer.Configurations.AutoMapper;
using RabbitMQ.Consumer.Infrastructure.Interfaces;
using RabbitMQ.Consumer.Infrastructure.Services;

namespace RabbitMQ.Consumer
{
    public class Program
    {
        private static IConfiguration _configuration = null;

        public static void Main(string[] args)
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var catalogConsumer = serviceProvider.GetService<ICatalogConsumerService>();
            catalogConsumer.ProductRevisionConsumer();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MongoConnection:ConnectionString"];
            var database = _configuration["MongoConnection:Database"];

            services.RegisterCommonServices();

            services.AddAutoMapper(Assembly.GetAssembly(typeof(ProductMapping)));
            Mapper.AssertConfigurationIsValid();

            services.Configure<MongoSettings>(
                options =>
                {
                    options.ConnectionString = connectionString;
                    options.Database = database;
                });

            var serviceProvider = services.BuildServiceProvider();

            var configurationRepository = serviceProvider.GetService<IConfigurationRepository>();

            //Get Environment Configurations
            var environment = _configuration.GetSection("MongoConnection:Environment").Value;
            var configurationDocument = configurationRepository.ReadConfiguration(environment).Result;

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
