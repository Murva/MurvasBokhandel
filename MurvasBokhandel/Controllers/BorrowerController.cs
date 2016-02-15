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
            foreach (MurvasBokhandel.Models.Mockup.BORROW b in mockup.borrow.borrows) {
                if (!(DateTime.Today > b.ToBeReturnedDate)) {
                    b.BorrowDate = DateTime.Today;
                    b.ToBeReturnedDate = DateTime.Today;
                    b.ToBeReturnedDate = b.ToBeReturnedDate.AddDays(7);
                }
            }
            return RedirectToAction("Start", mockup);
        }

        // TODO: ISBN istället, koppla ihop med böcker, status osv
        public ActionResult Reloan(int index) {
            mockup.borrow.borrows[index].BorrowDate = DateTime.Today;
            mockup.borrow.borrows[index].ToBeReturnedDate = DateTime.Today;
            mockup.borrow.borrows[index].ToBeReturnedDate = mockup.borrow.borrows[index].ToBeReturnedDate.AddDays(7);
            return RedirectToAction("Start", mockup);
        }
        public ActionResult Reloan() {
            return View(mockup);
        }
	}
}