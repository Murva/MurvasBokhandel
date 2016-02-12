using MurvasBokhandel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    public class BorrowerAdminController : Controller
    {
        // GET: BorrowerAdmin
        public ActionResult Start()
        {
            return View(Mockup.Borrowers);
        }

        public ActionResult Borrower(int id)
        {
            BorrowerWithBorrows br = new BorrowerWithBorrows()
            {
                Borrower = Mockup.Borrowers.Where(borrower => borrower.PersonId == id).First(),
                Borrows = Mockup.Borrows.Where(b => b.PersonId == id).ToList()
            };
            
            return View(br);
        }

        public ActionResult Update(Mockup.BORROWER Borrower)
        {
            Mockup.BORROWER b = Mockup.Borrowers.Where(borrower => borrower.PersonId == Borrower.PersonId).First();
            b = Borrower;

            return Redirect("Borrower/"+b.PersonId);
        }

        public ActionResult RenewLoan(int barcode, int personid)
        {
            Mockup.BORROW b = Mockup.Borrows.Where(borrow => (borrow.Barcode == barcode && borrow.PersonId == personid)).First();
            b.ToBeReturnedDate = b.ToBeReturnedDate.AddDays(7);

            return Redirect("Borrower/"+personid);
        }
    }
}