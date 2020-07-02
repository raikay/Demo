using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jwt.Demo.MS
{
    public class PolicyHandler : AuthorizationHandler<PolicyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {

            var http = (context.Resource as Microsoft.AspNetCore.Routing.RouteEndpoint);
            if (http == null)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            var questUrl = "/" + http.RoutePattern.RawText;
            var role = context.User.Claims.SingleOrDefault(s => s.Type == "Role").Value;
            //赋值用户权限
            var userPermissions = requirement.UserPermissions.Where(c=>c.Role== role);
            //是否经过验证
            var isAuthenticated = context.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                if (userPermissions.Any(u => u.Url == questUrl))
                {
                    //用户名
                    var userName = context.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier).Value;
                    

                    if (userPermissions.Any(w => w.UserName == userName))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
    public class PolicyRequirement : IAuthorizationRequirement
    {/// <summary>
     /// User rights collection
     /// </summary>
        public List<UserPermission> UserPermissions { get; private set; }
        /// <summary>
        /// No permission action
        /// </summary>
        public string DeniedAction { get; set; }
        /// <summary>
        /// structure
        /// </summary>
        public PolicyRequirement()
        {
            //Jump to this route without permission
            DeniedAction = new PathString("/api/nopermission");
            //Route configuration that users have access to, of course you can read it from the database, you can also put it in Redis for persistence
            UserPermissions = new List<UserPermission> {
                              new UserPermission {  Url="/api/val3", UserName="admin",Role="admin"},
                          };
        }
    }
    public class UserPermission
    {
        public string UserName { get; set; }
        public string Url { get; set; }
        public string Role { get; set; }
    }


    //public class DenyAnonymousAuthorizationRequirement : AuthorizationHandler<DenyAnonymousAuthorizationRequirement>, IAuthorizationRequirement
    //{
    //    /// <summary>
    //    /// Makes a decision if authorization is allowed based on a specific requirement.
    //    /// </summary>
    //    /// <param name="context">The authorization context.</param>
    //    /// <param name="requirement">The requirement to evaluate.</param>
    //    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DenyAnonymousAuthorizationRequirement requirement)
    //    {
    //        var user = context.User;
    //        var userIsAnonymous =
    //            user?.Identity == null ||
    //            !user.Identities.Any(i => i.IsAuthenticated);
    //        if (!userIsAnonymous)
    //        {
    //            context.Succeed(requirement);
    //        }
    //        return Task.CompletedTask;
    //    }
    //}
}
