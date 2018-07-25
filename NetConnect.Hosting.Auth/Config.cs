// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using NetConnect.Hosting.Core;
using System.Collections.Generic;
using System.Security.Claims;

namespace NetConnect.Hosting.Auth
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },

                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "https://localhost:44348/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44348/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                },
                 new Client
                {
                    ClientId = "app1",
                    ClientName = "App1",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "https://localhost:44342/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44342/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                },
                  new Client
                {
                    ClientId = "app2",
                    ClientName = "App2",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "https://localhost:44320/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44320/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "burak.basar",
                    Password = "123",

                    Claims = new List<Claim>
                    {
                        new Claim(NetConnectClaims.Name, "Burak"),
                        new Claim(NetConnectClaims.Lastname, "Basar"),
                        new Claim(NetConnectClaims.UserId, "1"),
                        new Claim(NetConnectClaims.Email, "burak@deneme.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "busra.kaya",
                    Password = "123",

                    Claims = new List<Claim>
                    {
                        new Claim(NetConnectClaims.Name, "Busra"),
                        new Claim(NetConnectClaims.Lastname, "Kaya"),
                        new Claim(NetConnectClaims.UserId, "2"),
                        new Claim(NetConnectClaims.Email, "busra@deneme.com")
                    }
                }
            };
        }
    }
}