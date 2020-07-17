using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.Server
{
    public class Ids4Config
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

        /// <summary>
        /// 获取api资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API #1")
            };
        }
        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // OpenID Connect隐式流客户端（MVC）
                new Client
                {
                    ClientId = "ClientA",
                    ClientName = "ClientA Name",
                    AllowedGrantTypes = GrantTypes.Implicit,//隐式方式
                    RequireConsent=false,//如果不需要显示否同意授权 页面 这里就设置为false
                    RedirectUris = { "http://localhost:50001/signin-oidc" },//登录成功后返回的客户端地址
                    PostLogoutRedirectUris = { "http://localhost:50001/signout-callback-oidc" },//注销登录后返回的客户端地址
                    

                    AllowedScopes =//下面这两个必须要加吧 不太明白啥意思
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new Client
                {
                    ClientId = "ClientB",
                    ClientName = "ClientB Name",
                    AllowedGrantTypes = GrantTypes.Implicit,//隐式方式
                    RequireConsent=false,//如果不需要显示否同意授权 页面 这里就设置为false
                    RedirectUris = { "http://localhost:50002/signin-oidc" },//登录成功后返回的客户端地址
                    PostLogoutRedirectUris = { "http://localhost:50002/signout-callback-oidc" },//注销登录后返回的客户端地址
                    

                    AllowedScopes =//下面这两个必须要加吧 不太明白啥意思
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new Client
                {
                    ClientId = "console client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes =GrantTypes.ResourceOwnerPassword, //GrantTypes.ClientCredentials,
                    AllowOfflineAccess = true,//允许刷新token
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                    AccessTokenLifetime=60*10,//token有效期
                    //AbsoluteRefreshTokenLifetime
                    AccessTokenType=AccessTokenType.Jwt,//AccessTokenType.Reference,
                    AllowedScopes = { "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new Client
                {
                    ClientId = "api2Id",
                    ClientName = "api2",
                    AllowedGrantTypes =GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                    AccessTokenLifetime=60*10,//token有效期

                    AllowedScopes = { "api2",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}
