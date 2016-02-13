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

        public ActionResult Borrower(long id = -1)
        {
            if (id == -1)
                return View("Start");

            BorrowerWithBorrows br = new BorrowerWithBorrows()
            {
                Borrower = Mockup.Borrowers.Where(borrower => borrower.PersonId == id).First(),
                Borrows = Mockup.Borrows.Where(b => b.PersonId == id).ToList()
            };

            return View(br);
        }

        public ActionResult Update(Mockup.BORROWER Borrower)
        {
            Mockup.Borrowers.Remove(Mockup.Borrowers.Where(borrower => borrower.PersonId == Borrower.PersonId).First());
            Mockup.Borrowers.Add(Borrower);

            return Redirect("/BorrowerAdmin/Borrower/" + Borrower.PersonId);
        }

        public ActionResult RenewLoan(long barcode, long personid)
        {
            Mockup.BORROW b = Mockup.Borrows.Where(borrow => (borrow.Barcode == barcode && borrow.PersonId == personid)).First();
            b.ToBeReturnedDate = b.ToBeReturnedDate.AddDays(7);

            return Redirect("/BorrowerAdmin/Borrower/"+personid);
        }
    }
}