using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MyFaculty.Identity.Configurations
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>()
            {
                new ApiScope("MyFacultyWebAPI", "WebAPI")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "roles",
                    DisplayName = "Roles",
                    UserClaims = { JwtClaimTypes.Role }
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>()
            {
                new ApiResource("MyFacultyWebAPI", "WebAPI", new [] { JwtClaimTypes.Name, JwtClaimTypes.Role })
                {
                    Scopes =
                    {
                        "MyFacultyWebAPI"
                    }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>()
            {
                new Client()
                {
                    ClientId = "my-faculty-web-api",
                    ClientName = "MyFacultyWebAPI",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets =
                    {
                        new Secret("2103476B-4916-40D4-A1F5-320214273BD5".Sha256())
                    },
                    RequireClientSecret = true,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://localhost:8080/signin-oidc",
                        "http://localhost:8080/silent-renew.html"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:8080"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:8080/signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "MyFacultyWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
