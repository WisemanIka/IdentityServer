using System.IO;
using Fox.Common.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Fox.Category
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var appSettingsConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            MyWebHostBuilderExtension.CreateWebHostBuilder(args, appSettingsConfiguration)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
