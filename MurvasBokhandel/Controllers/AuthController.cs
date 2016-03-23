using Common.Model;
using Common.Share;
using Repository.EntityModel;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (AuthService.Login(email, password))
            {
                Session["IsLoggedIn"] = PasswordService.CreateHash(Guid.NewGuid().ToString());
                Session["Permission"] = AuthService.GetRole(email).Name;
                BorrowerWithUser b = BorrowerService.GetBorrowerWithUserByEmail(email);
                Session["User"] = b;
                Auth.LoggedInUser = b;

                return Redirect("/");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return Redirect("/");
        }
    }
}