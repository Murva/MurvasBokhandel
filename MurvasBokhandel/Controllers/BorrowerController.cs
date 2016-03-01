using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Service;
using Common.Model;
using Repository.Repository;

namespace MurvasBokhandel.Controllers.Borrower
{
    public class BorrowerController : Controller
    {
        // GET: /Borrower/
        static private List<BorrowedBookCopy> BBC = BorrowService.GetBorrowedBooks("19111111-1111");
        
        public ActionResult Start()
        {   
            return View(BBC);
        }

        // TODO: Ändra om till BorrowedBookCopy
        public ActionResult ReloanAll() {
            foreach (BorrowedBookCopy b in BBC) {
                //b.borrow.BorrowDate = DateTime.Today;
                //b.borrow.ToBeReturnedDate = DateTime.Today.AddDays(7);
                BorrowService.updateBorrowDate(b.borrow);
                BorrowService.updateToBeReturnedDate(b.borrow);
            }
            return RedirectToAction("Start", BBC);
        }

        public ActionResult Reloan(int index) {
            //BBC[index].copy.StatusId = 1;
            //BBC[index].borrow.BorrowDate = DateTime.Today;
            //BBC[index].borrow.ToBeReturnedDate = DateTime.Today.AddDays(7); 
            BorrowService.updateBorrowDate(BBC[index].borrow);
            BorrowService.updateToBeReturnedDate(BBC[index].borrow);
            return View("Start", BBC);
        }
	}
}