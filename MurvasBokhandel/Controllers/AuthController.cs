using Common.Model.Base;
using Common;
using Services.Service;
using System;
using System.Web.Mvc;
using Common.Share;

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
                Auth.Login(BorrowerService.GetBorrowerWithUserByEmail(email));

                return Redirect("/");
            }

            ViewBag.Error = AlertView.Build("Fel email eller lösenord. Försök igen!", AlertType.Danger);

            return View();
        }

        public ActionResult Logout()
        {
            Auth.Logout();

            return Redirect("/");
        }
    }
}