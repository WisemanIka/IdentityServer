using System.Reflection;
using AutoMapper;
using Fox.Common.Configurations;
using Fox.Common.Infrastructure;
using Fox.Common.Logger;
using Fox.Common.Providers;
using Fox.Provider.Configurations.AutoMapper;
using Fox.Provider.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Fox.Provider
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
            services.AddCors(options =>
            {
                options.AddPolicy("ProviderCorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddAutoMapper(Assembly.GetAssembly(typeof(ProviderMapping)));
            Mapper.AssertConfigurationIsValid();

            //Register Custom Services
            services.RegisterServices();
            services.AddSingleton<IConfiguration>(Configuration);

            //Get Environment Configurations
            var environment = Configuration.GetSection("MongoConnection:Environment").Value;
            var configurationDocument = ConfigurationRepository.ReadConfiguration(environment).Result;

            //From Configuration Collection take only Provider Configs
            var dbConfiguration = configurationDocument.GetAs<ConfigurationDocument>("Provider");

            var connectionString = dbConfiguration.GetAs<string>("ConnectionString");
            var databaseName = dbConfiguration.GetAs<string>("Database");

            services.Configure<MongoSettings>(
                options =>
                {
                    options.ConnectionString = connectionString;
                    options.Database = databaseName;
                });

            var elasticSearchIndex = dbConfiguration.GetAs<string>("ElasticSearchIndex");
            var elasticSearchProvider = new ElasticSearchProvider(configurationDocument, elasticSearchIndex);
            var logger = new Logger(elasticSearchProvider);

            services.AddSingleton<ILogger>(logger);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Provider Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("ProviderCorsPolicy");

            //app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Provider Api V1");
            });
        }
    }
}
