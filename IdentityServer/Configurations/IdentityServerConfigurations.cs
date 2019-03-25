using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer.Configurations
{
    public static class IdentityServerConfigurations
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("basket", "Basket API Desc"),
                new ApiResource("ocelot", "API Gateway"), 
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "Angular",
                    ClientName = "Angular SPA",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedCorsOrigins = new [] { "http://localhost:4200/" },
                    ClientSecrets = {
                        new Secret("c0359956-eb75-480b-adde-2c33de5f3900".Sha256())
                    },
                    AllowedScopes = {
                        "basket"
                    }
                }
            };
        }
    }
}