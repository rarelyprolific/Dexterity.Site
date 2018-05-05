using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace Dexterity.Login.Configuration
{
    public static class TestUserConfiguration
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1357",
                    Username = "snake",
                    Password = "pass",
                    Claims = new List<Claim>
                    {
                        new Claim("name", "Solid Snake"),
                        new Claim("given_name", "Iroquois"),
                        new Claim("family_name", "Pliskin"),
                        new Claim("email", "solid.snake@metalgearsolid.world"),
                        new Claim("role", "Administrator"),
                        new Claim("weapon", "FAMAS"),
                        new Claim("weapon", "Desert Eagle"),
                        new Claim("armour", "Cardboard Box")
                    }
                }
            };
        }
    }
}
