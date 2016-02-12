using MurvasBokhandel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    public class BorrowerResult
    {
        public Mockup.BORROWER Borrower { get; set; }
        public List<Mockup.BORROW> Borrows { get; set; }
    }

    public class BorrowerAdminController : Controller
    {
        List<Mockup.BORROWER> Borrowers = new List<Mockup.BORROWER>() {
            new Mockup.BORROWER() {
                PersonId = 199007130355,
                FirstName = "Rikard",
                LastName = "Kungen",
                Address = "Rogbergavägen 17A, 56144 Huskvarna",
                Telno = "0702-642800"
            },
            new Mockup.BORROWER() {
                PersonId = 199305126632,
                FirstName = "Johan",
                LastName = "Tjejfors",
                Address = "MajsHansGatan 69, 666 66 Helvetet",
                Telno = "0702-6465135"
            },
            new Mockup.BORROWER() {
                PersonId = 198965482354,
                FirstName = "Adam",
                LastName = "Rasmusen",
                Address = "Rogbergavägen 17A, 56144 Huskvarna",
                Telno = "0702-642800"
            },
            new Mockup.BORROWER() {
                PersonId = 192501321654,
                FirstName = "David",
                LastName = "Milsson",
                Address = "Rogbergavägen 17A, 56144 Huskvarna",
                Telno = "0702-642800"
            }
        };

        List<Mockup.BORROW> Borrows = new List<Mockup.BORROW>() {
            new Mockup.BORROW() {
                Barcode = 123456789,
                PersonId = 199007130355,
                BorrowDate = new DateTime(2016,02,11),
                ReturnDate = new DateTime(),
                ToBeReturnedDate = new DateTime(2016,02,21)
            },
            new Mockup.BORROW() {
                Barcode = 123456790,
                PersonId = 199007130355,
                BorrowDate = new DateTime(2016,02,11),
                ReturnDate = new DateTime(),
                ToBeReturnedDate = new DateTime(2016,02,21)
            }
        };
        // GET: BorrowerAdmin
        public ActionResult Start()
        {
            return View(Borrowers);
        }

        public ActionResult Borrower(int id)
        {
            BorrowerResult br = new BorrowerResult()
            {
                Borrower = Borrowers.Where(borrower => borrower.PersonId == id).First(),
                Borrows = Borrows.Where(b => b.PersonId == id).ToList()
            };
            
            return View(br);
        }

        public ActionResult Update(Mockup.BORROWER Borrower)
        {
            Mockup.BORROWER b = Borrowers.Where(borrower => borrower.PersonId == Borrower.PersonId).First();
            b = Borrower;

            return Redirect("Borrower/"+b.PersonId);
        }

        public ActionResult RenewLoan(int barcode, int personid)
        {
            Mockup.BORROW b = Borrows.Where(borrow => (borrow.Barcode == barcode && borrow.PersonId == personid)).First();
            b.ToBeReturnedDate = b.ToBeReturnedDate.AddDays(7);

            return Redirect("Borrower/"+personid);
        }
    }
}