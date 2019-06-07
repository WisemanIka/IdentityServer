using AutoMapper;
using Fox.BasketApi.Configurations.AutoMapper;
using Fox.BasketApi.Extensions;
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
using System.Reflection;

namespace Fox.BasketApi
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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("BasketCorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddAutoMapper(Assembly.GetAssembly(typeof(BasketMapping)));
            Mapper.AssertConfigurationIsValid();

            //Register Custom Services
            services.RegisterServices();
            services.AddSingleton<IConfiguration>(Configuration);

            //Get Environment Configurations
            var environment = Configuration.GetSection("MongoConnection:Environment").Value;
            var configurationDocument = ConfigurationRepository.ReadConfiguration(environment).Result;

            //From Configuration Collection take only Product Configs
            var dbConfiguration = configurationDocument.GetAs<ConfigurationDocument>("Basket");

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1";
                option.InstanceName = "master";
            });


            var elasticSearchIndex = dbConfiguration.GetAs<string>("ElasticSearchIndex");
            var elasticSearchProvider = new ElasticSearchProvider(configurationDocument, elasticSearchIndex);
            var logger = new Logger(elasticSearchProvider);

            services.AddSingleton<ILogger>(logger);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Catalog Api", Version = "v1" });
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

            app.UseCors("BasketCorsPolicy");

            //app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Catalog Api V1");
            });
        }
    }
}
