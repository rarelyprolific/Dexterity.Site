using IdentityServer4.Models;
using System.Collections.Generic;

namespace Dexterity.Login.Configuration
{
    public static class IdentityResourceConfiguration
    {
        public static IEnumerable<IdentityResource> Get()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "permissions",
                    UserClaims = { "role" }
                },
                new IdentityResource
                {
                    Name = "equipment",
                    UserClaims = { "weapon", "armour" }
                }
            };
        }
    }
}
