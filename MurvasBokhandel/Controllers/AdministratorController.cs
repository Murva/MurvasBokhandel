using Common;
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
            if (Auth.HasAdminPermission())
                return View();
                
            return Redirect("/");
        }
    }
}