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
                new ApiResource("ocelot", "API Gateway"),
                new ApiResource("basket", "Basket API Desc")  { ApiSecrets = { new Secret("test".Sha256()) } }
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
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris = { "https://www.getpostman.com/oauth2/callback" },
                    PostLogoutRedirectUris = { "https://www.getpostman.com" },
                    AllowedCorsOrigins = new [] { "http://localhost:4200/" },
                    ClientSecrets = {
                        new Secret("c0359956-eb75-480b-adde-2c33de5f3900".Sha256())
                    },
                    AllowedScopes = {
                        "ocelot"
                    }
                },
                new Client
                {
                    ClientId = "Postman",
                    ClientName = "Postman Test Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris = { "https://www.getpostman.com/oauth2/callback" },
                    PostLogoutRedirectUris = { "https://www.getpostman.com" },
                    AllowedCorsOrigins = { "https://www.getpostman.com" },
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        "ocelot"
                    }
                }
            };
        }
    }
}