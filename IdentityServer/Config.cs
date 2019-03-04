// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


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
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("OcelotAPI", "Ocelot Gateway API"),
                //new ApiResource("ProductAPI", "Product API")
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
                    ClientSecrets = {
                        new Secret("c0359956-eb75-480b-adde-2c33de5f3900".Sha256())
                    },
                    AllowedScopes = {
                        "OcelotAPI",
                        //"ProductAPI"
                    }
                }
            };
        }
    }
}