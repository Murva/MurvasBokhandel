using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Start()
        {
            if (Session["Permission"] as string == "Admin"){
                return View();
            }
            else
            {
                return Redirect("/");
            }
            
        }
    }
}