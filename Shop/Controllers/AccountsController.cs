using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;
using System.Web.Security;

namespace Shop.Controllers
{
    public class AccountsController : Controller
    {
        private ShopEntities db = new ShopEntities();
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User u)
        {
            if (ModelState.IsValid)
            {
                string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(u.password, "SHA1");
                string pass1 = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "MD5");
                u.password = pass1;
                var user = db.Users.Where(x => x.name == u.name && x.password == u.password).Count();
                if (user > 0)
                {
                    FormsAuthentication.SetAuthCookie(u.name, false);
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ViewBag.msg = "username and or password not match.";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}