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
            user u = (user)Session["User"];
            BBC = BorrowService.GetBorrowedBooks(u.PersonId);
            return View(BBC);
        }

        public ActionResult ReloanAll() {
            foreach (BorrowedBookCopy b in BBC) {
                if (b.fine==0) {
                    BorrowService.updateBorrowDate(b.borrow);
                    BorrowService.updateToBeReturnedDate(b.borrow, b.category.Period);    
                }
            }
            return RedirectToAction("Start", BBC);
        }

        public ActionResult Reloan(int index) {
            
            BorrowService.updateBorrowDate(BBC[index].borrow);
            BorrowService.updateToBeReturnedDate(BBC[index].borrow, BBC[index].category.Period );
            return View("Start", BBC);
        }
        public ActionResult GetAcountInfo()
        {
            Repository.EntityModel.user user = (Repository.EntityModel.user)Session["User"];        
            BorrowerWithUser activeUser = BorrowerService.GetBorrowerWithUserByPersonId(user.PersonId);
            
            //BorrowerWithUser activeUser = new BorrowerWithUser();
            return View(activeUser);
        }

        [HttpPost]
        public ActionResult Update(user user, borrower borrower)
        {
            BorrowerWithUser borrowerWithUser = new BorrowerWithUser();
            borrowerWithUser.User = user;
            borrowerWithUser.Borrower = borrower;
            borrowerWithUser.Borrower.PersonId = user.PersonId;
            UserService.update(borrowerWithUser);
            Session["User"] = AuthService.GetUser(borrowerWithUser.User.Email);//Denna måste nog ändras

            // + user.Borrower.PersonId
            return Redirect("/User/GetAcountInfo/");
                        
        }
	}
}