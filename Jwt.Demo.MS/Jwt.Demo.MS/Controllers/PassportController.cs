using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Demo.MS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassportController : ControllerBase
    {


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get(string userName, string pwd)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(pwd))
            {
                
                return Ok(new
                {
                    token =TokenHelper.GetToken(new UserDto { Id=Guid.NewGuid(),Name=userName })
                });
            }
            else
            {
                return BadRequest(new { message = "username or password is incorrect." });
            }
        }


        //var getCookie = "";
        //HttpContext.Request.Cookies.TryGetValue("guid1", out getCookie);

        //HttpContext.Response.Cookies.Append("guid1", Guid.NewGuid().ToString(), new CookieOptions { Domain = "raikay.com" });
        //HttpContext.Response.Cookies.Append("guid2", Guid.NewGuid().ToString());
        //HttpContext.Response.Cookies.Append("guid3", Guid.NewGuid().ToString());

    }
}
