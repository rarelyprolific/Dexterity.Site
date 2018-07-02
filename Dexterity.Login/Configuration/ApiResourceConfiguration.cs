using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dexterity.Login.Configuration
{
    public static class ApiResourceConfiguration
    {
        public static IEnumerable<ApiResource> Get()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "api1",
                    Scopes =
                    {
                        new Scope()
                        {
                            Name = "api1scope"
                        }
                    },
                    UserClaims =
                    {
                        IdentityServerConstants.StandardScopes.Email,
                        "weapon"
                    }

                }
            };
        }
    }
}
