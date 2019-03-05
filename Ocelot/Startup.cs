using System;
using IdentityServer4.AccessTokenValidation;
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
            services.AddAuthentication()
                .AddIdentityServerAuthentication("IdentityServiceApiKey", options =>
                {
                    options.Authority = "http://localhost:8080";
                    options.ApiName = "BasketAPI";
                    options.RequireHttpsMetadata = false;
                    //options.SupportedTokens = SupportedTokens.Both;
                    //options.ApiSecret = "c0359956-eb75-480b-adde-2c33de5f3900";
                    //options.Events = new JwtBearerEvents
                    //{
                    //    OnAuthenticationFailed = async ctx =>
                    //    {
                    //        int i = 0;
                    //    },
                    //    OnTokenValidated = async ctx =>
                    //    {
                    //        int i = 0;
                    //    }
                    //};
                });

            services.AddOcelot(Configuration);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();

            app.UseOcelot().Wait();
        }
    }
}
