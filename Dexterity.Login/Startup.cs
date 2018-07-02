using Dexterity.Login.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dexterity.Login
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentityServer(options =>
            {
                // Set the lifetime of the authentication cookie to 5 seconds
                options.Authentication.CookieLifetime = TimeSpan.FromSeconds(5);
            })
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(ClientConfiguration.Get())
                .AddInMemoryIdentityResources(IdentityResourceConfiguration.Get())
                .AddInMemoryApiResources(ApiResourceConfiguration.Get())
                .AddTestUsers(TestUserConfiguration.Get());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }
    }
}
