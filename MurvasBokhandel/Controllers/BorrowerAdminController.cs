//using MurvasBokhandel.Models;
using Common.Model;
using Repository.EntityModel;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    public class BorrowerAdminController : Controller
    {
        static private List<BorrowedBookCopy> BBC = new List<BorrowedBookCopy>();
        
        public ActionResult Start()
        {
            if (Session["Permission"] as string == "Admin")
            {
                return View(BorrowerService.getBorrowers());
            }
            return Redirect("/Error/Code/403");
        }

        // Lägger till användarkonto till en borrower
        public ActionResult AddUser(user u)
        {
            if (Session["Permission"] as string == "Admin") {
                if (ModelState.IsValid && (u.RoleId == 1 || u.RoleId == 2))
                {
                    AuthService.CreateUser(u);
                    return Redirect("/BorrowerAdmin/Borrower/" + u.PersonId);
                }
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        ViewBag.error += ", " + error.ErrorMessage;
                    }
                }
                return View("Borrower", BorrowerService.GetBorrower(u.PersonId));
            }
            return Redirect("/Error/Code/403");
        }

        [HttpGet]
        public ActionResult Borrower(string id)
        {
            if (Session["Permission"] as string == "Admin") {
                return View(BorrowerService.GetBorrower(id));
            }
            return Redirect("/Error/Code/403");
        }

        // Används för att uppdatera en borrower
        [HttpPost]
        public ActionResult Borrower(BorrowerWithUser BorrowerWithUser)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (ModelState.IsValid && (BorrowerWithUser.Borrower.CategoryId == 1 ||
                                             BorrowerWithUser.Borrower.CategoryId == 2 ||
                                             BorrowerWithUser.Borrower.CategoryId == 3 ||
                                             BorrowerWithUser.Borrower.CategoryId == 4))
                {
                    BorrowerService.UpdateBorrower(BorrowerWithUser.Borrower);
                    return RedirectToAction("/Borrower/" + BorrowerWithUser.Borrower.PersonId);
                }
                return View(BorrowerService.GetBorrower(BorrowerWithUser.Borrower.PersonId));
            }
            return Redirect("/Error/Code/403");
        }
     
        // Tar bort en borrower och konto om det finns
        public ActionResult Remove(BorrowerWithBorrows bwb)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BorrowerService.RemoveBorrower(bwb.BorrowerWithUser.Borrower);
                return Redirect("Start");
            }
            return Redirect("/Error/Code/403");
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
            return Redirect("/Error/Code/403");
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
            return Redirect("/Error/Code/403");
        }

        // Sparar en ny borrower till databasen
        public ActionResult Store(BorrowerAndCategories baci)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (ModelState.IsValid && !BorrowerService.checkIfBorrowerExists(baci.borrower.PersonId) && (baci.CatergoryId == 1 ||
                                             baci.CatergoryId == 2 ||
                                             baci.CatergoryId == 3 ||
                                             baci.CatergoryId == 4))
                {
                    borrower b = new borrower();
                    b = baci.borrower;
                    b.CategoryId = baci.CatergoryId;
                    BorrowerService.StoreBorrower(b);
                    return Redirect("Start");
                }
                else {
                    ViewBag.idExists = "Detta personnumret är redan registrerat hos oss";
                    BorrowerAndCategories bac = new BorrowerAndCategories();
                    bac.borrower = new borrower();
                    bac.categories = CategoryService.getCategories();
                    return View("Create", bac);
                } 
            }
            return Redirect("/Error/Code/403");
        }
    }
}