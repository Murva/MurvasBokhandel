using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Service;
using Common.Model;
using Repository.Repository;
using Repository.EntityModel;

namespace MurvasBokhandel.Controllers.User
{
    public class UserController : Controller
    {
        // GET: /Borrower/        
        
        public ActionResult Start() {
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
        public ActionResult ReloanAll() {
            if (Session["Permission"] as string != null)
            {
                //OBS! Hämta lån innan
                ActiveAndHistoryBorrows borrows = new ActiveAndHistoryBorrows();
                BorrowerWithUser b = (BorrowerWithUser) Session["User"];

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
                BorrowService.RenewLoad(bwu.Borrower, borrows.active[index].borrow.Barcode);
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
        public ActionResult GetAcountInfo(user user, borrower borrower)
        {
            if (Session["Permission"] as string != null)
            {
                if (ModelState.IsValid) 
                {

                    Repository.EntityModel.user activeUser = (Repository.EntityModel.user)Session["User"];

                    if (Services.Service.UserService.emailExists(user.Email) && (!(activeUser.Email == user.Email)))
                    {
                        ViewBag.Error = "Epostadressen finns redan registrerad."; // denna går inte just nu!!!!!                        
                        BorrowerWithUser someOneElseEmail = BorrowerService.GetBorrowerWithUserByPersonId(activeUser.PersonId);

                        return View(someOneElseEmail);   
                    }

                    BorrowerWithUser borrowerWithUser = new BorrowerWithUser();
                    borrowerWithUser.User = user;
                    borrowerWithUser.Borrower = borrower;
                    borrowerWithUser.Borrower.PersonId = user.PersonId;
                    UserService.update(borrowerWithUser);
                    Session["User"] = AuthService.GetUserByPersonId(user.PersonId);//Denna måste nog ändras

                    return Redirect("/User/GetAcountInfo/");
                }
                else
                {
                    BorrowerWithUser original = (BorrowerWithUser)Session["User"];

                    return View(BorrowerService.GetBorrowerWithUserByPersonId(original.User.PersonId));
                }
            }
            return View();               
        }
	}
}