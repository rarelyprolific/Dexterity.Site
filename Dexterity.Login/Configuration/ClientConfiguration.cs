﻿using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

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
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:44370/signin-oidc" },
                    PostLogoutRedirectUris = new List<string> { "http://localhost:44370" },
                    RequireConsent = false,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "permissions",
                        "equipment"
                    }
                }
            };
        }
    }
}