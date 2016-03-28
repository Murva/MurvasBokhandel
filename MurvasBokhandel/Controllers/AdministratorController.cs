using Common;
using Common.Model;
using MurvasBokhandel.Controllers.Share;
using Services.Service;
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
            if (new Auth((BorrowerWithUser)Session["User"]).HasAdminPermission())
                return View();
                
            return Redirect("/");
        }
    }
}