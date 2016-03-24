using Common.Share;
using Services.Service;
using System;
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
                Session["User"] = Auth.LoggedInUser = BorrowerService.GetBorrowerWithUserByEmail(email);

                return Redirect("/");
            }

            ViewBag.Error = AlertView.Build("Fel email eller lösenord. Försök igen!", "danger");

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return Redirect("/");
        }
    }
}