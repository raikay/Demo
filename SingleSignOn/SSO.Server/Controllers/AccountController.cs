using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
//using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SSO.Server.Data;
using Microsoft.AspNetCore.Http;

namespace SSO.Server.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// 登录post回发处理
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            User user = new User
            {
                CreateDate = DateTime.Now,
                Id = "999",
                Password = password,
                Remark = "remark",
                UserName = userName,
                
            };//await _adminService.GetByStr(userName, password);
            if (user != null)
            {
                Microsoft.AspNetCore.Authentication.AuthenticationProperties props = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(1))
                };
                await HttpContext.SignInAsync(user.Id.ToString(), user.UserName, props);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }

                return View();
            }
            else
            {
                return View();
            }
        }
    }
}
