using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SSO.Server.Data;

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
            if (string.IsNullOrEmpty(userName)||string.IsNullOrEmpty(password))
            {
                return View();
            }
            User user = new Data.User
            {
                CreateDate = DateTime.Now,
                Id = 1,
                Password = password,
                Remark = "remark",
                UserName = userName
            };
            if (user != null)
            {
                AuthenticationProperties props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(1))
                };
                //await HttpContext.SignInAsync(user.Id.ToString(), user.UserName, props);
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
