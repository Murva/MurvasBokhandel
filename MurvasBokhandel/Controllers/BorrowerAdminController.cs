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
        }

        public ActionResult Borrower(string id)
        {
            BorrowerWithBorrows br = new BorrowerWithBorrows();
            br = BorrowerService.GetBorrower(id);
            return View(br);
        }

        public ActionResult Update(borrower Borrower)
        {
            BorrowerService.UpdateBorrower(Borrower);
            return Redirect("/BorrowerAdmin/Borrower/" + Borrower.PersonId);
        }

        public ActionResult Remove(borrower Borrower)
        {
            BorrowerService.RemoveBorrower(Borrower);
            return Redirect("Start");
        }

        public ActionResult RenewLoan(string barcode, string personid)
        {
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
            BorrowerAndCategories bac = new BorrowerAndCategories();
            bac.borrower = new borrower();
            bac.categories = CategoryService.getCategories();
            return View(bac);
        }

        public ActionResult Store(BorrowerAndCategories baci)
        {
            borrower b = new borrower();
            b = baci.borrower;
            b.CategoryId = baci.CatergoryId;
            BorrowerService.StoreBorrower(b);
            return Redirect("Start");
        }
    }
}