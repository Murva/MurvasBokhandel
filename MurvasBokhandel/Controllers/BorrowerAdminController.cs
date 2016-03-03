//using MurvasBokhandel.Models;
using Common.Model;
using Repository.EntityModel;
using Services.Service;
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
            return View(BorrowerService.getBorrowers());
            //return View(Mockup.Borrowers);
        }

        public ActionResult Borrower(string PersonId)
        {
            //BorrowerWithBorrows br = new BorrowerWithBorrows()
            //{
            //    Borrower = Mockup.Borrowers.Where(borrower => borrower.PersonId == Convert.ToInt32(PersonId)).First(),
            //    Borrows = Mockup.Borrows.Where(b => b.PersonId == Convert.ToInt32(PersonId)).ToList()
            //};

            //return View(br);
            BorrowerWithBorrows br = new BorrowerWithBorrows();
            br = BorrowerService.GetBorrower(PersonId);
            return View(br);
        }

        public ActionResult Update(borrower Borrower)
        {
            //Mockup.Borrowers.Remove(Mockup.Borrowers.Where(borrower => borrower.PersonId == Borrower.PersonId).First());
            //Mockup.Borrowers.Add(Borrower);

            BorrowerService.UpdateBorrower(Borrower);
            return Redirect("/BorrowerAdmin/Borrower/" + Borrower.PersonId);
        }

        public ActionResult Remove(borrower Borrower)
        {
            //Mockup.Borrowers.Remove(Mockup.Borrowers.Where(b => b.PersonId == Borrower.PersonId).First());
            BorrowerService.RemoveBorrower(Borrower);
            return Redirect("Start");
        }

        public ActionResult RenewLoan(string barcode, string personid)
        {
            //Mockup.BORROW b = Mockup.Borrows.Where(borrow => (borrow.Barcode == barcode && borrow.PersonId == personid)).First();
            //b.ToBeReturnedDate = b.ToBeReturnedDate.AddDays(7);
            BorrowerWithBorrows b = BorrowerService.GetBorrower(personid);
            foreach (borrow borrow in b.Borrows) {
                if (borrow.Barcode == barcode)
                    BorrowService.updateBorrowDate(borrow);
                    BorrowService.updateToBeReturnedDate(borrow);
            }

            return Redirect("/BorrowerAdmin/Borrower/"+personid);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Store(borrower Borrower)
        {
            //Mockup.Borrowers.Add(Borrower);
            BorrowerService.StoreBorrower(Borrower);
            return Redirect("Start");
        }
    }
}