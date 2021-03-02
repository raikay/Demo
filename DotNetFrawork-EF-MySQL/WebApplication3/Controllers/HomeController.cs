using ClassLibrary1;
using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var _db=new MySqlContext())
            {

                var user1 = new User
                {
                    Id = 3,
                    Name = "张三3号"
                };

                var s1 = _db.Users.Add(user1);
                var s2 = _db.SaveChanges();
                var ss = _db.Users.Find(1);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}