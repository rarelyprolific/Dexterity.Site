using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Dexterity.Site
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Clear the Microsoft claim 
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            // Prevent the automagical mapping JWT claim types to ASP Identity claim types
            // (We'll explicitly set how we want the mapping to happen in options.TokenValidationParameters below)
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.Authority = "http://localhost:64833"; // The host name of the identity server
                options.RequireHttpsMetadata = false; // Remove this and serve over HTTPS in production

                options.ClientId = "Dexterity.Site";
                options.ClientSecret = "secret";
                options.SaveTokens = true; // I think we'll need this to store access and refresh tokens when using Hybrid flow

                // Set response type to Hybrid flow
                options.ResponseType = "code id_token";

                // Causes the client to make a request to the userinfo endpoint instead of expecting claims to be in the id_token
                options.GetClaimsFromUserInfoEndpoint = true;

                // Ensure the ASP.NET authentication session life matches that of the authentication token issued by Identity Server
                options.UseTokenLifetime = true;

                // Add the scopes we want to consume in this client (we get "openid" and "profile" automatically)
                options.Scope.Add("email");
                options.Scope.Add("permissions");
                options.Scope.Add("equipment");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Maps the "name" claim from the JWT to User.Identity.Name
                    NameClaimType = "name",
                    // Maps the "role" claim from the JWT to user roles (e.g. User.IsInRole("Administrator")
                    RoleClaimType = "role"
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
