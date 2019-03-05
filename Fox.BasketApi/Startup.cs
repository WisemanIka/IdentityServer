using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Fox.BasketApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication()
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:8080";
                    options.ApiName = "BasketApi";
                    options.RequireHttpsMetadata = false;
                    //options.SupportedTokens = SupportedTokens.Both;
                    //options.ApiSecret = "c0359956-eb75-480b-adde-2c33de5f3900";
                });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseAuthentication();
        }
    }
}
