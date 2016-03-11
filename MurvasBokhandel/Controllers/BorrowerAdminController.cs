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
        static private List<BorrowedBookCopy> BBC = new List<BorrowedBookCopy>();
        
        // GET: BorrowerAdmin
        
        public ActionResult Start()
        {
            if (Session["Permission"] as string == "Admin")
            {
                return View(BorrowerService.getBorrowers());
            }
            else
            {
                return Redirect("/");
            }
        }


        public ActionResult AddUser(BorrowerWithBorrows b, String PersonId){
            if (Session["Permission"] as string == "Admin")
            {
                b.BorrowerWithUser.User.PersonId = PersonId;
                AuthService.CreateUser(b.BorrowerWithUser.User);
                return Redirect("/BorrowerAdmin/");
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Borrower(string id)
        {
            if (Session["Permission"] as string == "Admin")
            {
                return View(BorrowerService.GetBorrower(id));
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Update(borrower Borrower)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BorrowerService.UpdateBorrower(Borrower);
                return Redirect("/BorrowerAdmin/Borrower/" + Borrower.PersonId);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Remove(BorrowerWithBorrows bwb)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BorrowerService.RemoveBorrower(bwb.BorrowerWithUser.Borrower);
                return Redirect("Start");
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult RenewLoan(string barcode, string personid)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BorrowerWithBorrows b = BorrowerService.GetBorrower(personid);
                
                BBC = BorrowService.GetBorrowedBooks(personid);
                foreach (borrow borrow in b.Borrows)
                {
                    if (borrow.Barcode == barcode)
                    {
                        BorrowService.updateBorrowDate(borrow);
                        BorrowService.updateToBeReturnedDate(borrow, BBC[0].category.Period);
                    }
                }

                return Redirect("/BorrowerAdmin/Borrower/" + personid);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Create()
        {
            if (Session["Permission"] as string == "Admin")
            {
                BorrowerAndCategories bac = new BorrowerAndCategories();
                bac.borrower = new borrower();
                bac.categories = CategoryService.getCategories();
                return View(bac);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Store(BorrowerAndCategories baci)
        {
            if (Session["Permission"] as string == "Admin")
            {
                borrower b = new borrower();
                b = baci.borrower;
                b.CategoryId = baci.CatergoryId;
                BorrowerService.StoreBorrower(b);
                return Redirect("Start");
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}