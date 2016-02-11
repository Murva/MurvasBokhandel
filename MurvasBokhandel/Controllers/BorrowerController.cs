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
        Mockup mockup = new Mockup();
        //
        // GET: /Borrower/
        public ActionResult Start()
        {
            return View(mockup);
        }
        public ActionResult ReloanAll() {
            foreach (MurvasBokhandel.Models.Mockup.BORROWEDBOOK b in mockup.books) {
                b.borrow.BorrowDate = DateTime.Today;
            }
            return RedirectToAction("/Borrower/Start", mockup);
        }
	}
}