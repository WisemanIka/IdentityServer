using Fox.Common.Configurations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fox.Common.Extensions
{
    public static class MyWebHostBuilderExtension
    {
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
                });
    }
}
