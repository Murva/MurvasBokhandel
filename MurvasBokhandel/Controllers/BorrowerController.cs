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
        //static private List<BorrowedBookCopy> BBC = BorrowService.GetBorrowedBooks("19111111-1111");
        static private List<BorrowedBookCopy> BBC = BorrowService.GetBorrowedBooks("19630328-2267");
        //static private List<BorrowedBookCopy> BBC = BorrowService.GetBorrowedBooks("19790229-1116");
        //static private List<BorrowedBookCopy> BBC = BorrowService.GetBorrowedBooks("19630328-2267");
        //static private List<BorrowedBookCopy> BBC = BorrowService.GetBorrowedBooks("19630328-2267");
        //static private List<BorrowedBookCopy> BBC = BorrowService.GetBorrowedBooks("19920227-5468");
        
        public ActionResult Start() {
            return View(BBC);
        }

        public ActionResult ReloanAll() {
            foreach (BorrowedBookCopy b in BBC) {
                BorrowService.updateBorrowDate(b.borrow);
                BorrowService.updateToBeReturnedDate(b.borrow);
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