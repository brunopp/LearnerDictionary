using LearnerDictionary.Models;
using LearnerDictionary.Models.ViewModels;
using LearnerDictionary.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace LearnerDictionary.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            var ctx = HttpContext.GetOwinContext();
            var authManager = ctx.Authentication;

            var userManager = new UserManager<IUser>(new LDRepository());

            var user = userManager.FindById(model.Username);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var identity = userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie).Result;

            authManager.SignIn(new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddDays(14) }, identity);

            return Redirect("/");
        }
    }
}