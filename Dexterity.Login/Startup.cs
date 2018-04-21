using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dexterity.Login.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Dexterity.Login
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddIdentityServer(options =>
            //{
            //    options.UserInteraction.LoginUrl = "http://localhost:64833/Account/Login";
            //})

            //// TODO: Figure out how I should be using IdentityResources and/or ApiResources
            ////       An IdentityResource allows you to model a scope that will return a certain set of claims
            ////       An APIResource allows you to model access to a protected resource (typically an API)

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddTestUsers(IdentityServerConfiguration.GetUsers());

            //// TODO: Use /.well-known/openid-configuration to verify scope and claim configuration

            services.AddMvc();
            //// TODO: Add AddMvc(options => { options.Filters.Add(new RequireHttpsAttribute()) });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            //// TODO: Add app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301, 44343));

            app.UseMvcWithDefaultRoute();
        }
    }
}
