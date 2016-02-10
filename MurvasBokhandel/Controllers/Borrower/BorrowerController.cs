using MurvasBokhandel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers.Borrower
{
    public class BorrowerController : Controller
    {
        //
        // GET: /Borrower/
        public ActionResult Start()
        {
            Mockup mockup = new Mockup();
            return View(mockup);
        }
	}
}