using System.IO;
using Fox.Common.Configurations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Fox.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Fox.Catalog
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var appSettingsConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            CreateWebHostBuilder(args, appSettingsConfiguration).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfigurationRoot appSettingsConfiguration) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    //Take Mongo Connection String for Configuration Database
                    var connectionString = appSettingsConfiguration["MongoConnection:ConnectionString"];
                    var database = appSettingsConfiguration["MongoConnection:Database"];

                    //Register Common Services (MongoDB, HttpContext etc...)
                    services.RegisterCommonServices();

                    services.Configure<MongoSettings>(
                        options =>
                        {
                            options.ConnectionString = connectionString;
                            options.Database = database;
                        });
                })
                .UseStartup<Startup>();
    }
}
