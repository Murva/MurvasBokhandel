using Common;
using Common.Model;
using Common.Share;
using Repository.EntityModel;
using Repository.Validation;
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
                BorrowerWithBorrows b = BorrowerService.GetBorrowerWithBorrows(u.PersonId);

                if (ModelState.IsValid)
                {
                    if (PasswordValidaton.IsValid(u.Password))
                    {
                        if (!UserService.EmailExists(u.Email))
                        {
                            AuthService.CreateUser(u);
                            return RedirectToAction("Borrower", new { id = u.PersonId });
                        }

                        Auth.PushAlert(AlertView.Build("Konto med emailen " + u.Email + " existerar. Ange en annan!", AlertType.Danger));

                        return View("Borrower", b);
                    }

                    Auth.PushAlert(AlertView.Build(PasswordValidaton.ErrorMessage, AlertType.Danger));

                    return RedirectToAction("Borrower", new { id = u.PersonId });
                }


                Auth.PushAlert(AlertView.BuildErrors(ViewData));

                return RedirectToAction("Borrower", new { id = u.PersonId });
            }

            return Redirect("/Error/Code/403");
        }

        [HttpGet]
        public ActionResult Borrower(string id)
        {
            if (Auth.HasAdminPermission())
            {
                if (!BorrowerService.BorrowerExists(id))
                    return Redirect("/Error/Code/404");

                if (Auth.LoggedInUser.User.PersonId == id)
                    return Redirect("/User/GetAcountInfo");

                return View(BorrowerService.GetBorrowerWithBorrows(id));
            }
            
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
                    user tempU = AuthService.GetUserByPersonId(BorrowerWithUser.User.PersonId);
                    if (BorrowerWithUser.User.Email != null && !(UserService.EmailExists(BorrowerWithUser.User.Email) && BorrowerWithUser.User.Email != tempU.Email)) {
                                UserService.Update(BorrowerWithUser, null);
                    }
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
                    Auth.PushAlert(AlertView.Build("Det gick inte att ta bort låntagare. Kontrollera att inga aktiva lån finns.", AlertType.Danger));

                    return RedirectToAction("Borrower", new { id = bwb.BorrowerWithUser.Borrower.PersonId });
                }

                Auth.PushAlert(AlertView.Build("Låntagare med PersonId "+bwb.BorrowerWithUser.Borrower.PersonId + " är nu borttagen", AlertType.Success));
                
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

        [HttpGet]
        public ActionResult Create()
        {
            if (Auth.HasAdminPermission())
            {
                return View(new BorrowerAndCategories()
                {
                    Borrower = new borrower(),
                    Categories = CategoryService.GetCategories()
                });
            }
            return Redirect("/Error/Code/403");
        }

        // Sparar en ny borrower till databasen
        [HttpPost]
        public ActionResult Create(BorrowerAndCategories baci)
        {
            if (Auth.HasAdminPermission())
            {
                baci.Categories = CategoryService.GetCategories();

                if (ModelState.IsValid && (baci.CatergoryId == 1 || 
                                             baci.CatergoryId == 2 ||
                                             baci.CatergoryId == 3 ||
                                             baci.CatergoryId == 4))
                {
                    if (!BorrowerService.BorrowerExists(baci.Borrower.PersonId))
                    {

                        borrower b = new borrower();
                        b = baci.Borrower;
                        b.CategoryId = baci.CatergoryId;
                        BorrowerService.StoreBorrower(b);

                        Auth.PushAlert(AlertView.Build("Låntagare " + baci.Borrower.FirstName + " " + baci.Borrower.LastName + " skapad.", AlertType.Success));

                        return Redirect("Start");
                    }

                    baci.PushAlert(AlertView.Build("Detta personnumret är redan registrerat hos oss", AlertType.Danger));
                    return View(baci);
                }
                
                return View(baci);
            }

            return Redirect("/Error/Code/403");
        }
    }
}