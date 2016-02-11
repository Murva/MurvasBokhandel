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
        static Mockup mockup = new Mockup();
        //
        // GET: /Borrower/
        public ActionResult Start()
        {
            return View(mockup);
        }
        public ActionResult ReloanAll() {
            foreach (MurvasBokhandel.Models.Mockup.BORROWEDBOOK b in mockup.books) {
                if (!(DateTime.Today > b.borrow.ToBeReturnedDate)) {
                    b.borrow.BorrowDate = DateTime.Today;
                    b.borrow.ToBeReturnedDate = DateTime.Today;
                    b.borrow.ToBeReturnedDate = b.borrow.ToBeReturnedDate.AddDays(7);
                }
            }
            return RedirectToAction("Start", mockup);
        }
        public ActionResult Reloan(int index) {
            mockup.books[index].borrow.BorrowDate = DateTime.Today;
            mockup.books[index].borrow.ToBeReturnedDate = DateTime.Today;
            mockup.books[index].borrow.ToBeReturnedDate = mockup.books[index].borrow.ToBeReturnedDate.AddDays(7);
            return RedirectToAction("Start", mockup);
        }
	}
}