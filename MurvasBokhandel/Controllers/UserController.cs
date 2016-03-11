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
        static private List<BorrowedBookCopy> BBC = new List<BorrowedBookCopy>();

        public ActionResult Start() {
            if (Session["Permission"] as string != null)
            {
                user u = (user)Session["User"];
                BBC = BorrowService.GetBorrowedBooks(u.PersonId);
                return View(BBC);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult ReloanAll() {
            if (Session["Permission"] as string != null)
            {
                foreach (BorrowedBookCopy b in BBC)
                {
                    if (b.fine == 0)
                    {
                        BorrowService.updateBorrowDate(b.borrow);
                        BorrowService.updateToBeReturnedDate(b.borrow, b.category.Period);
                    }
                }
                return RedirectToAction("Start", BBC);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Reloan(int index) 
        {
            if (Session["Permission"] as string != null) 
            {
                BorrowService.updateBorrowDate(BBC[index].borrow);
                BorrowService.updateToBeReturnedDate(BBC[index].borrow, BBC[index].category.Period);
                return View("Start", BBC);
            }
            else
            {
                return Redirect("/");
            }
        }
        public ActionResult GetAcountInfo()
        {
            if (Session["Permission"] as string != null) {
                Repository.EntityModel.user user = (Repository.EntityModel.user)Session["User"];        
                BorrowerWithUser activeUser = BorrowerService.GetBorrowerWithUserByPersonId(user.PersonId);
            
                //BorrowerWithUser activeUser = new BorrowerWithUser();
                return View(activeUser);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public ActionResult Update(user user, borrower borrower)
        {
            BorrowerWithUser borrowerWithUser = new BorrowerWithUser();
            borrowerWithUser.User = user;
            borrowerWithUser.Borrower = borrower;
            borrowerWithUser.Borrower.PersonId = user.PersonId;
            UserService.update(borrowerWithUser);
            Session["User"] = AuthService.GetUserByPersonId(user.PersonId);//Denna måste nog ändras

            // + user.Borrower.PersonId
            return Redirect("/User/GetAcountInfo/");
                        
        }
	}
}