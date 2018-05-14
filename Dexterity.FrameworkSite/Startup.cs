using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(Dexterity.FrameworkSite.Startup))]

namespace Dexterity.FrameworkSite
{
    public class Startup
    {
        // Story so far:
        // 1. install-package Microsoft.Owin.Host.SystemWeb
        // 2. Added OWIN Startup from "Add new item" dialog to generate this file.
        // 3. install-package Microsoft.Owin.Security.OpenIdConnect
        // 4. install-package Microsoft.Owin.Security.Cookies

        public void Configuration(IAppBuilder app)
        {
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                SignInAsAuthenticationType = "Cookies",

                Authority = "http://localhost:64833", // The host name of the identity server
                RequireHttpsMetadata = false, // Remove this and serve over HTTPS in production

                ClientId = "Dexterity.FrameworkSite",
                RedirectUri = "http://localhost:57056/signin-oidc",
                ResponseType = "id_token",  // "id_token" alone signifies "Implicit" flow
                Scope = "openid profile",

                //// TODO: No claims coming through yet!? Figure out why! Also, how can I see the id_token to see if they are there?

                //TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                //{
                //    NameClaimType = "name",
                //    RoleClaimType = "role",
                //},

                //// TODO: Look at example at https://github.com/IdentityServer/IdentityServer3.Samples/tree/master/source/MVC%20Authentication/EmbeddedMvc
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var nid = new ClaimsIdentity(
                            n.AuthenticationTicket.Identity.AuthenticationType,
                            "given_name",
                            "role");
                        //Constants.ClaimTypes.GivenName,
                        //Constants.ClaimTypes.Role);

                        // get userinfo data
                        //var userInfoClient = new UserInfoClient(
                        //    new Uri(n.Options.Authority + "/connect/userinfo"),
                        //    n.ProtocolMessage.AccessToken);

                        //var userInfo = await userInfoClient.GetAsync();
                        //userInfo.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Item1, ui.Item2)));

                        // keep the id_token for logout
                        nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                        // add access token for sample API
                        //nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

                        // keep track of access token expiration
                        //nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));

                        // add some other app specific claim
                        nid.AddClaim(new Claim("app_specific", "some data"));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            nid,
                            n.AuthenticationTicket.Properties);
                    },

                    //RedirectToIdentityProvider = n =>
                    //{
                    //    if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                    //    {
                    //        var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                    //        if (idTokenHint != null)
                    //        {
                    //            n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                    //        }
                    //    }

                    //    return Task.FromResult(0);
                    //}
                }
            });


            // ************************************************************
            // **** This is what worked in the .NET Core 2 application ****
            // ************************************************************

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = "oidc";
            //})
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddOpenIdConnect("oidc", options =>
            //{
            //    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            //    options.Authority = "http://localhost:64833"; // The host name of the identity server
            //    options.RequireHttpsMetadata = false; // Remove this and serve over HTTPS in production

            //    options.ClientId = "Dexterity.Site";
            //    options.SaveTokens = true; // I think we'll need this to store access and refresh tokens when using Hybrid flow

            //    // Add the scopes we want to consume in this client (we get "openid" and "profile" automatically)
            //    options.Scope.Add("email");
            //    options.Scope.Add("permissions");
            //    options.Scope.Add("equipment");

            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        // Maps the "name" claim from the JWT to User.Identity.Name
            //        NameClaimType = "name",
            //        // Maps the "role" claim from the JWT to user roles (e.g. User.IsInRole("Administrator")
            //        RoleClaimType = "role"
            //    };
            //});
        }
    }
}
