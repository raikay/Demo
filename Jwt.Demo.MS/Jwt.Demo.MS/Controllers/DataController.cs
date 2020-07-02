using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Demo.MS.Controllers
{
    [ApiController]
   // [Route("[controller]")]
    public class DataController : ControllerBase
    {


        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// 这个接口登陆过的都能访问
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/val2")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get2()
        {

            var auth = HttpContext.AuthenticateAsync().Result.Principal.Claims;
            var userName = auth.FirstOrDefault(t => t.Type.Equals(ClaimTypes.Name))?.Value;
            return new string[] { "这个接口登陆过的都能访问", $"userName={userName}" };
        }

        /// <summary>
        /// 这个接口有管理员权限才可以访问
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/val3")]
        [Authorize("Permission")]
        public ActionResult<IEnumerable<string>> Get3()
        {

            //这是获取自定义参数的方法
            var auth = HttpContext.AuthenticateAsync().Result.Principal.Claims;
            var userName = auth.FirstOrDefault(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            var role = auth.FirstOrDefault(t => t.Type.Equals("Role"))?.Value;
            return new string[] { "这个接口有管理员权限才可以访问", $"userName={userName}", $"Role={role}" };
        }
    }
}
