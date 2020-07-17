using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForDotNet.Auth
{
    /// <summary>
    /// 我的校验逻辑
    /// </summary>
    public class MyResourceOwnerValidator : IResourceOwnerPasswordValidator
    {
        public MyResourceOwnerValidator()
        {
            // 可以注入服务
        }

        /// <summary>
        /// 校验方法
        /// </summary>
        /// <param name="context">上下文信息(包含了用户名密码等信息)</param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            await Task.Run(() =>
            {
                //校验逻辑...

                // 校验成功
                if (context.UserName == "admin" && context.Password == "1234")
                {
                    context.Result = new GrantValidationResult(Guid.NewGuid().ToString(), "DIY", new List<Claim>()
                    {
                        new Claim ("DIYClaim","This is DIYClaim"),
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere")

                    });
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "认证失败",
                                           new Dictionary<string, object>()
                                           {
                                                { "Test","This Is Test" }
                                           });
                }
            });
        }

    }
}
