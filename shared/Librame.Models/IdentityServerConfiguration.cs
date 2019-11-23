using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Librame.Models
{
    public class IdentityServerConfiguration
    {
        public static ApiResource DefaultApiResource
            => new ApiResource("Api", "Librame.AspNetCore.IdentityServer.Api");

        public static Client DefaultClient
            => new Client
            {
                ClientId = "Client",
                ClientName = "Librame.AspNetCore.IdentityServer.Client",
                AllowedGrantTypes = GrantTypes.Implicit, // OpenID Connect 隐式流客户端
                RequireConsent = false, // 如果不需要显示否同意授权页面，这里就设置为 false
                RedirectUris = { "https://localhost:44386/Home/Index" }, //登录成功后返回的客户端地址
                PostLogoutRedirectUris = { "https://localhost:44386/Logout" }, //注销登录后返回的客户端地址

                //AllowOfflineAccess = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    //IdentityServerConstants.StandardScopes.OfflineAccess,
                    "Api"
                }
            };

        public static IList<IdentityResource> DefaultIdentityResources
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
    }
}
