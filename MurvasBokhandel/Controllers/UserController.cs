using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Service;
using Common.Model;
using Repository.Repository;
using Repository.EntityModel;
using Common.Share;

namespace MurvasBokhandel.Controllers.User
{
    public class UserController : Controller
    {   
        public ActionResult Start() 
        {
            if (Session["Permission"] as string != null)
            {
                ActiveAndHistoryBorrows borrows = new ActiveAndHistoryBorrows();
                BorrowerWithUser u = (BorrowerWithUser)Session["User"];
                borrows.active = BorrowService.GetActiveBorrowedBooks(u.User.PersonId);
                borrows.history = BorrowService.GetHistoryBorrowedBooks(u.User.PersonId);
                return View(borrows);
            }
            return Redirect("/");
        }

        // Lånar om de böcker som är möjliga att låna om
        public ActionResult ReloanAll() 
        {
            if (Session["Permission"] as string != null)
            {
                //OBS! Hämta lån innan
                ActiveAndHistoryBorrows borrows = new ActiveAndHistoryBorrows();
                
                BorrowerWithUser b = (BorrowerWithUser) Session["User"];
                borrows.active = BorrowService.GetActiveBorrowedBooks(b.User.PersonId);
                borrows.history = BorrowService.GetHistoryBorrowedBooks(b.User.PersonId);
                BorrowService.RenewAllLoans(b.Borrower, borrows.active);

                return RedirectToAction("Start", borrows);
            }
            return Redirect("/");
        }

        // Lånar om enskild bok
        public ActionResult Reloan(int index) 
        {
            if (Session["Permission"] as string != null) 
            {
                ActiveAndHistoryBorrows borrows = new ActiveAndHistoryBorrows();
                BorrowerWithUser bwu = (BorrowerWithUser) Session["User"];
                borrows.active = BorrowService.GetActiveBorrowedBooks(bwu.User.PersonId);
                borrows.history = BorrowService.GetHistoryBorrowedBooks(bwu.User.PersonId);
                BorrowService.RenewLoad(bwu.Borrower, borrows.active[index].borrow.Barcode);
                return View("Start", borrows);
            }
            return Redirect("/");
        }
        [HttpGet]
        public ActionResult GetAcountInfo()
        {
            if (Session["Permission"] as string != null) {
                BorrowerWithUser user = (BorrowerWithUser)Session["User"];        
                BorrowerWithUser activeUser = BorrowerService.GetBorrowerWithUserByPersonId(user.User.PersonId);
                return View(activeUser);
            }
            return Redirect("/");
        }
              
        [HttpPost]
        public ActionResult GetAcountInfo(user user, borrower borrower, string newpassword = null)
        {
            if (Session["Permission"] as string != null)
            {
                if (ModelState.IsValid)
                {
                    if (PasswordService.VerifyPassword(user.Password, Auth.LoggedInUser.User.Password))
                    {
                        if (UserService.emailExists(user.Email) && (!(Auth.LoggedInUser.User.Email == user.Email)))
                        {
                            ViewBag.Error = "Epostadressen finns redan registrerad.";

                            return View(Auth.LoggedInUser);
                        }

                        BorrowerWithUser borrowerWithUser = new BorrowerWithUser();
                        borrowerWithUser.User = user;
                        borrowerWithUser.Borrower = borrower;
                        borrowerWithUser.Borrower.PersonId = user.PersonId;

                        if (newpassword == "")
                            UserService.update(borrowerWithUser, user.Password);
                        else
                            UserService.update(borrowerWithUser, newpassword);

                        Session["User"] = Auth.LoggedInUser = BorrowerService.GetBorrowerWithUserByPersonId(user.PersonId);

                        return Redirect("/User/GetAcountInfo/");
                    }

                    ViewBag.Error = "Du måste ange ditt lösenord.";
                    return View(Auth.LoggedInUser);
                }
                return View(Auth.LoggedInUser);
            }
            return Redirect("/Error/Code/403");               
        }
	}
}