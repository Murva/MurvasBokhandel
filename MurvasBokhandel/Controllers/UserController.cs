﻿using System;
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
            if (Auth.HasUserPermission())
                return View(UserService.GetActiveAndHistoryBorrows());

            return Redirect("/Error/Code/403");
        }

        // Lånar om de böcker som är möjliga att låna om
        public ActionResult ReloanAll() 
        {
            if (Auth.HasUserPermission())
            {
                //OBS! Hämta lån innan
                ActiveAndHistoryBorrows borrows = UserService.GetActiveAndHistoryBorrows();
                BorrowService.RenewAllLoans(Auth.LoggedInUser.Borrower, borrows.Active);

                return RedirectToAction("Start", borrows);
            }
            return Redirect("/Error/Code/403");
        }

        // Lånar om enskild bok
        public ActionResult Reloan(int index) 
        {
            if (Auth.HasUserPermission()) 
            {
                ActiveAndHistoryBorrows borrows = UserService.GetActiveAndHistoryBorrows();
                BorrowService.RenewLoad(Auth.LoggedInUser.Borrower, borrows.Active[index].borrow.Barcode);

                return View("Start", borrows);
            }
            return Redirect("/Error/Code/403");
        }
        [HttpGet]
        public ActionResult GetAcountInfo()
        {
            if (Auth.HasUserPermission())
                return View(BorrowerService.GetBorrowerWithUserByPersonId(Auth.LoggedInUser.User.PersonId));

            return Redirect("/Error/Code/403");
        }
              
        [HttpPost]
        public ActionResult GetAcountInfo(user user, borrower borrower, string newpassword = null)
        {
            if (Auth.HasUserPermission())
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
                            UserService.Update(borrowerWithUser, user.Password);
                        else
                            UserService.Update(borrowerWithUser, newpassword);

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