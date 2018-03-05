using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Dexterity.Site
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //// TODO: Add AddMvc(options => { options.Filters.Add(new RequireHttpsAttribute()) });

            //// TODO: Local login example: services.AddAuthentication(options => {
            ////                                               options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthencticationScheme
            ////                                               options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme
            ////                                               options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme
            ////                                               })
            ////                                    .AddCookies(options => { options.LoginPath = "/auth/signin"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //// TODO: Add app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301, 44343));

            //// TODO: UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
