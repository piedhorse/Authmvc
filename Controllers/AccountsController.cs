using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Authmvc.Models;
using System.Linq;

namespace Authmvc.Controllers
{
    public class AccountsController : Controller
    {   
        registeredusersEntities entity = new registeredusersEntities();

        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel credenctials)
        {
            bool userExist = entity.UsersTbls.Any(x =>x.Email == credenctials.Email && x.Passcode == credenctials.Password );
            UsersTbl u = entity.UsersTbls.FirstOrDefault(x => x.Email == credenctials.Email && x.Passcode == credenctials.Password);
            if (userExist) {
                FormsAuthentication.SetAuthCookie(u.Username, false);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Username or password is wrong");
            return View();
        }
        [HttpPost]
        public ActionResult Signup(UsersTbl userinfo)
        {
            entity.UsersTbls.Add(userinfo);
            entity.SaveChanges();

            return RedirectToAction("Login");
        } 

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}