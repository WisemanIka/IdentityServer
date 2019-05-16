using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ocelot.Gateway
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var key = "IdentityServiceApiKey";

            services.AddAuthentication()
                .AddIdentityServerAuthentication(key, options =>
                {
                    options.Authority = "http://localhost:8080";
                    options.ApiName = "ocelot";
                    options.RequireHttpsMetadata = false;
                });

            services.AddOcelot();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseOcelot().Wait();
        }
    }
}
