using System.Reflection;
using AutoMapper;
using Fox.Common.Configurations;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Logger;
using IdentityServer.Configurations;
using IdentityServer.Configurations.AutoMapper;
using IdentityServer.Infrastructure;
using IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace IdentityServer
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            this.Environment = environment;
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.-_";

                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapper(Assembly.GetAssembly(typeof(BaseMapping)));
            Mapper.AssertConfigurationIsValid();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfigurations.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfigurations.GetApiResources())
                .AddInMemoryClients(IdentityServerConfigurations.GetClients())
                .AddAspNetIdentity<User>();

            var emailSenderService = Configuration.GetSection("EmailSenderSettings").Get<EmailSenderSettings>();
            services.RegisterEmailService(emailSenderService);

            services.RegisterCommonServices();
            services.AddScoped<IAccountService, AccountService>();
            services.AddSingleton<IConfiguration>(Configuration);

            var logger = new Logger();
            services.AddSingleton<ILogger>(logger);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "IdentityServer Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpContext();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My IdentityServer Api V1");
            });
        }
    }
}