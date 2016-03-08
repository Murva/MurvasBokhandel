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
                    BorrowService.updateToBeReturnedDate(b.borrow);    
                }
            }
            return RedirectToAction("Start", BBC);
        }

        public ActionResult Reloan(int index) {
            BorrowService.updateBorrowDate(BBC[index].borrow);
            BorrowService.updateToBeReturnedDate(BBC[index].borrow);
            return View("Start", BBC);
        }
	}
}