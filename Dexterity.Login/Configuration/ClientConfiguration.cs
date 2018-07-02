using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Dexterity.Login.Configuration
{
    public static class ClientConfiguration
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Dexterity.Site",
                    ClientName = "The Dexterity ASP.NET Core website",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    RedirectUris = { "https://localhost:44363/site/signin-oidc" },
                    PostLogoutRedirectUris = new List<string> { "https://localhost:44363/site" },

                    // Set the lifetime of the identity token to 5 seconds
                    IdentityTokenLifetime = 5,

                    RequireConsent = false,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "permissions",
                        "equipment"
                    }
                },

                new Client
                {
                    ClientId = "Dexterity.FrameworkSite",
                    ClientName = "The Dexterity ASP.NET Framework website",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:57056/signin-oidc" },
                    PostLogoutRedirectUris = new List<string> { "http://localhost:57056" },
                    RequireConsent = false,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "permissions",
                        "equipment"
                    }
                },

                new Client
                {
                    ClientId = "Dexterity.AutoThenticator",
                    ClientName = "The Dexterity Desktop Test Application",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("autothen".ToSha256())
                    },
                    AlwaysSendClientClaims = true,
                    Claims = new List<Claim>
                    {
                        new Claim("email", "just something random here")
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "permissions",
                        "equipment",
                        "api1scope"
                    }
                }
            };
        }
    }
}
