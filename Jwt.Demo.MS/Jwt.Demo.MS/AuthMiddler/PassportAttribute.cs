using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
namespace Jwt.Demo.MS
{
    public class PassportAttribute : ActionFilterAttribute, IExceptionFilter
    {

        /// <summary>
        /// 在控制器执行之前调用
        /// </summary>
        /// <param name="context">执行的上下文</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {

        }

        /// <summary>
        /// 在控制器执行之后调用
        /// </summary>
        /// <param name="context">执行的上下文</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;

            if (isAuthenticated)
            {

                var jwtToken = TokenHelper.GetToken(new UserDto
                {
                    Id = Guid.Parse(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value),
                    Name = context.HttpContext.User.Identity.Name
                });

                context.HttpContext.Response.Headers.Add("Authorization", jwtToken);
                context.HttpContext.Response.Cookies.Append("Authorization", jwtToken);


            };

        }

        /// <summary>
        /// 在返回数据执行之前调用
        /// </summary>
        /// <param name="context">执行的上下文</param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {

        }

        /// <summary>
        /// 在返回数据执行之后调用
        /// </summary>
        /// <param name="context">执行的上下文</param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {

        }

        /// <summary>
        /// 当然是发生异常时被调用了
        /// </summary>
        /// <param name="context">执行的上下文</param>
        public void OnException(ExceptionContext context)
        {

        }

    }
}
