using MurvasBokhandel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers.Borrower
{
    public class BorrowerController : Controller
    {
        static List<BorrowedBookCopy> BBC = new List<BorrowedBookCopy>()
        {
            new BorrowedBookCopy(){
                book = Mockup.AuthorsWithBooksResults[0].Books[0],
                author = Mockup.AuthorsWithBooksResults[0].Author,
                copy = Mockup.Copies[0],
                borrow = Mockup.Borrows[0]
            },
            new BorrowedBookCopy(){
                book = Mockup.AuthorsWithBooksResults[3].Books[0],
                author = Mockup.AuthorsWithBooksResults[3].Author,
                copy = Mockup.Copies[1],
                borrow = Mockup.Borrows[1]
            }
        };
        //
        // GET: /Borrower/
        public ActionResult Start()
        {
            return View(BBC);
        }

        // TODO: Ändra om till BorrowedBookCopy
        public ActionResult ReloanAll() {
            foreach (BorrowedBookCopy b in BBC) {
                b.borrow.BorrowDate = DateTime.Today;
                b.borrow.ToBeReturnedDate = DateTime.Today.AddDays(7);
            }
            return RedirectToAction("Start", BBC);
        }

        public ActionResult Reloan(int index) {
            BBC[index].copy.StatusId = 1;
            BBC[index].borrow.BorrowDate = DateTime.Today;
            BBC[index].borrow.ToBeReturnedDate = DateTime.Today.AddDays(7); 
            return View("Start", BBC);
        }
	}
}