using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("BasketAPI", "Basket API Desc")
                //new ApiResource
                //{
                //    Name = "BasketApi",
                //    DisplayName = "Basket API",
                //    ApiSecrets =
                //    {
                //        new Secret("c0359956-eb75-480b-adde-2c33de5f3900".Sha256())
                //    }
                //}
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                //new Client
                //{
                //    ClientId = "Angular",
                //    ClientName = "Angular SPA",
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    ClientSecrets = {
                //        new Secret("c0359956-eb75-480b-adde-2c33de5f3900".Sha256())
                //    },
                //    AllowedScopes = {
                //        "BasketAPI",
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile
                //    }
                //}
            };
        }
    }
}