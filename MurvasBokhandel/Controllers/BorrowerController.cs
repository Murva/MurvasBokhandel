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
        static List<BorrowedBookCopy> BBC = new List<BorrowedBookCopy>();
        //
        // GET: /Borrower/
        public ActionResult Start()
        {
            return View(BBC);
        }

        // TODO: Ändra om till BorrowedBookCopy
        public ActionResult ReloanAll() {
            foreach (MurvasBokhandel.Models.Mockup.BORROW b in mockup.borrow.borrows) {
                if (!(DateTime.Today > b.ToBeReturnedDate)) {
                    b.BorrowDate = DateTime.Today;
                    b.ToBeReturnedDate = DateTime.Today;
                    b.ToBeReturnedDate = b.ToBeReturnedDate.AddDays(7);
                }
            }
            return RedirectToAction("Start", BBC);
        }

        // TODO: ISBN istället, koppla ihop med böcker, status osv
        public ActionResult Reloan(int index) {
            mockup.borrow.borrows[index].BorrowDate = DateTime.Today;
            mockup.borrow.borrows[index].ToBeReturnedDate = DateTime.Today;
            mockup.borrow.borrows[index].ToBeReturnedDate = mockup.borrow.borrows[index].ToBeReturnedDate.AddDays(7);
            return RedirectToAction("Start", mockup);
        }
        public ActionResult Reloan(BorrowedBookCopy bbc) {
            bbc.status.statusid = 1;
            bbc.borrows.BorrowDate = DateTime.Today;
            bbc.borrows.ToBeReturnedDate = DateTime.Today;
            bbc.borrows.ToBeReturnedDate = bbc.borrows.ToBeReturnedDate.AddDays(7);
            return View("Start", BBC);
        }
	}
}