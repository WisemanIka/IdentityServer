using System.Reflection;
using AutoMapper;
using Fox.Category.Configurations.AutoMapper;
using Fox.Category.Extensions;
using Fox.Common.Configurations;
using Fox.Common.Infrastructure;
using Fox.Common.Logger;
using Fox.Common.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Fox.Category
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
                options.AddPolicy("CategoryCorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddAutoMapper(Assembly.GetAssembly(typeof(CategoryMapping)));
            Mapper.AssertConfigurationIsValid();

            //Register Custom Services
            services.RegisterServices();
            services.AddSingleton<IConfiguration>(Configuration);

            //Get Environment Configurations
            var environment = Configuration.GetSection("MongoConnection:Environment").Value;
            var configurationDocument = ConfigurationRepository.ReadConfiguration(environment).Result;

            //From Configuration Collection take only Category Configs
            var productConfiguration = configurationDocument.GetAs<ConfigurationDocument>("Category");

            var productConnectionString = productConfiguration.GetAs<string>("ConnectionString");
            var productDatabase = productConfiguration.GetAs<string>("Database");

            services.Configure<MongoSettings>(
                options =>
                {
                    options.ConnectionString = productConnectionString;
                    options.Database = productDatabase;
                });

            var elasticSearchIndex = productConfiguration.GetAs<string>("ElasticSearchIndex");
            var elasticSearchProvider = new ElasticSearchProvider(configurationDocument, elasticSearchIndex);
            var logger = new Logger(elasticSearchProvider);

            services.AddSingleton<ILogger>(logger);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Category Api", Version = "v1" });
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

            app.UseCors("CategoryCorsPolicy");

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Category Api V1");
            });
        }
    }
}
