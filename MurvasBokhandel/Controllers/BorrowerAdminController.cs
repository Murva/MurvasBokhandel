//using MurvasBokhandel.Models;
using Common.Model;
using Common.Share;
using MurvasBokhandel.Controllers.Share;
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
        public ActionResult Start(string letter = "A")
        {
            if (Auth.HasAdminPermission() && LetterLists.LetterList.Contains(letter))
            {
                return View(new LettersAndBorrowers(LetterLists.LetterList, BorrowerService.GetBorrowersByLetter(letter))) ;
            }
            return Redirect("/Error/Code/403");
        }

        // Lägger till användarkonto till en borrower
        public ActionResult AddUser(user u)
        {
            if (Auth.HasAdminPermission())
            {
                if (ModelState.IsValid)
                {
                    AuthService.CreateUser(u);
                    return Redirect("/BorrowerAdmin/Borrower/" + u.PersonId);
                }

                ViewBag.error = AlertView.BuildErrors(ViewData);
                return View("Borrower", BorrowerService.GetBorrowerWithBorrows(u.PersonId));
            }

            return Redirect("/Error/Code/403");
        }

        [HttpGet]
        public ActionResult Borrower(string id)
        {
            if (Auth.HasAdminPermission())
                return View(BorrowerService.GetBorrowerWithBorrows(id));
            
            return Redirect("/Error/Code/403");
        }

        // Används för att uppdatera en borrower
        [HttpPost]
        public ActionResult Borrower(BorrowerWithUser BorrowerWithUser)
        {
            if (Auth.HasAdminPermission())
            {
                if (ModelState.IsValid && (BorrowerWithUser.Borrower.CategoryId == 1 ||
                                             BorrowerWithUser.Borrower.CategoryId == 2 ||
                                             BorrowerWithUser.Borrower.CategoryId == 3 ||
                                             BorrowerWithUser.Borrower.CategoryId == 4))
                {
                    BorrowerService.UpdateBorrower(BorrowerWithUser.Borrower);
                    return RedirectToAction("/Borrower/" + BorrowerWithUser.Borrower.PersonId);
                }
                return View(BorrowerService.GetBorrowerWithBorrows(BorrowerWithUser.Borrower.PersonId));
            }
            return Redirect("/Error/Code/403");
        }
     
        // Tar bort en borrower och konto om det finns
        public ActionResult Remove(BorrowerWithBorrows bwb)
        {
            if (Auth.HasAdminPermission())
            {
                if (!BorrowerService.RemoveBorrower(bwb.BorrowerWithUser.Borrower))
                {
                    TempData["Error"] = "Det gick inte att ta bort låntagare. Kontrollera att inga aktiva lån finns.";
                    return Redirect("/BorrowerAdmin/Borrower/"+bwb.BorrowerWithUser.Borrower.PersonId);
                }
                
                return Redirect("Start");
            }
            return Redirect("/Error/Code/403");
        }

        public ActionResult RenewLoan(string barcode, string personid, int index)
        {
            if (Auth.HasAdminPermission())
            {
                ActiveAndHistoryBorrows borrows = new ActiveAndHistoryBorrows();
                borrows.Active = BorrowService.GetActiveBorrowedBooks(personid);
                BorrowService.RenewLoad(BorrowerService.GetBorrower(personid), borrows.Active[index].borrow.Barcode);
           
                return Redirect("/BorrowerAdmin/Borrower/" + personid);
            }
            return Redirect("/Error/Code/403");
        }

        public ActionResult Create()
        {
            if (Auth.HasAdminPermission())
            {
                return View(new BorrowerAndCategories()
                {
                    borrower = new borrower(),
                    categories = CategoryService.getCategories()
                });
            }
            return Redirect("/Error/Code/403");
        }

        // Sparar en ny borrower till databasen
        public ActionResult Store(BorrowerAndCategories baci)
        {
            if (Auth.HasAdminPermission())
            {
                if (ModelState.IsValid && !BorrowerService.CheckIfBorrowerExists(baci.borrower.PersonId) && (baci.CatergoryId == 1 ||
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