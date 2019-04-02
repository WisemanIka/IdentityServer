using Fox.Common.Configurations;
using Fox.Common.Configurations.RabbitMQ;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Logger;
using Fox.Common.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMQ.Consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IConfigurationRepository configurationRepository)
        {
            Configuration = configuration;
            ConfigurationRepository = configurationRepository;
        }

        public IConfiguration Configuration { get; }
        public IConfigurationRepository ConfigurationRepository { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}
