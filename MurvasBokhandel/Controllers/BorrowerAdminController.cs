//using MurvasBokhandel.Models;
using Common.Model;
using Repository.EntityModel;
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
        static private List<BorrowedBookCopy> BBC = new List<BorrowedBookCopy>();
        
        // GET: BorrowerAdmin
        
        public ActionResult Start()
        {
            if (Session["Permission"] as string == "Admin")
            {
                return View(BorrowerService.getBorrowers());
            }
            else
            {
                return Redirect("/");
            }
        }


        public ActionResult AddUser(user u, String PersonId){
            if (Session["Permission"] as string == "Admin")
            {
                //if (b.BorrowerWithUser.User.Email != null && b.BorrowerWithUser.User.Password != null &&
                    //ModelState.IsValidField(b.BorrowerWithUser.User.Email) && ModelState.IsValidField(b.BorrowerWithUser.User.Password))
                if(ModelState.IsValid)
                {
                    //u.PersonId = PersonId;
                    AuthService.CreateUser(u);
                    //ViewBag.UserInput("");
                    return RedirectToAction("/Borrower/" + PersonId);
                }
                else {
                    //ViewBag.UserInput("FEL INMATNING ROFFELMJAOU");
                    return RedirectToAction("/Borrower/" + PersonId);
                }   
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpGet]
        public ActionResult Borrower(string id)
        {
            if (Session["Permission"] as string == "Admin")
            {
                return View(BorrowerService.GetBorrower(id));
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public ActionResult Borrower(BorrowerWithUser BorrowerWithUser)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (ModelState.IsValid)
                {
                    BorrowerService.UpdateBorrower(BorrowerWithUser.Borrower);
                    return RedirectToAction("/Borrower/" + BorrowerWithUser.Borrower.PersonId);
                }
                else
                {
                    return View(BorrowerService.GetBorrower(BorrowerWithUser.Borrower.PersonId));
                }
            }
            else
            {
                return Redirect("/");
            }
        }
     
        public ActionResult Remove(BorrowerWithBorrows bwb)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BorrowerService.RemoveBorrower(bwb.BorrowerWithUser.Borrower);
                return Redirect("Start");
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult RenewLoan(string barcode, string personid)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BorrowerWithBorrows b = BorrowerService.GetBorrower(personid);
                
                BBC = BorrowService.GetBorrowedBooks(personid);
                foreach (borrow borrow in b.Borrows)
                {
                    if (borrow.Barcode == barcode)
                    {
                        BorrowService.updateBorrowDate(borrow);
                        BorrowService.updateToBeReturnedDate(borrow, BBC[0].category.Period);
                    }
                }

                return Redirect("/BorrowerAdmin/Borrower/" + personid);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Create()
        {
            if (Session["Permission"] as string == "Admin")
            {
                BorrowerAndCategories bac = new BorrowerAndCategories();
                bac.borrower = new borrower();
                bac.categories = CategoryService.getCategories();
                return View(bac);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Store(BorrowerAndCategories baci)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (ModelState.IsValid && !BorrowerService.checkIfBorrowerExists(baci.borrower.PersonId))
                {
                    //string pIdReg = "[1-2][0-9]{3}[0-1][1-9][0-3][1-9][-][0-9]{4}";
                    //Regex re = new Regex(pIdReg);
                    List<borrower> borrowers = BorrowerService.getBorrowers();
                    foreach (borrower borr in borrowers)
                    {
                        if (borr.PersonId == baci.borrower.PersonId)
                            return Redirect("Start");
                    }
                    borrower b = new borrower();
                    b = baci.borrower;
                    b.CategoryId = baci.CatergoryId;
                    BorrowerService.StoreBorrower(b);
                    return Redirect("Start");
                }
                else {
                    BorrowerAndCategories bac = new BorrowerAndCategories();
                    bac.borrower = new borrower();
                    bac.categories = CategoryService.getCategories();
                    return View("Create", bac);
                } 
                //return Redirect("Start");
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}