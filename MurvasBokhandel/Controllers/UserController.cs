using Common;
using Common.Model;
using Common.Share;
using Repository.EntityModel;
using Repository.Validation;
using Services.Service;
using System.Web.Mvc;


namespace MurvasBokhandel.Controllers.User
{
    public class UserController : Controller
    {
        public ActionResult Start() 
        {
            Auth auth = new Auth((BorrowerWithUser)Session["User"]);
            if (auth.HasUserPermission())
            {
                return View(UserService.GetActiveAndHistoryBorrows(auth.LoggedInUser.User.PersonId));
            }

            return Redirect("/Error/Code/403");
        }

        /// <summary>
        /// Reloans all books possible
        /// </summary>
        /// <returns></returns>
        public ActionResult ReloanAll() 
        {
            Auth auth = new Auth((BorrowerWithUser)Session["User"]);
            if (auth.HasUserPermission())
            {
                ActiveAndHistoryBorrows borrows = UserService.GetActiveAndHistoryBorrows(auth.LoggedInUser.User.PersonId);
                BorrowService.RenewAllLoans(auth.LoggedInUser.Borrower, borrows.Active);

                return RedirectToAction("Start", borrows);
            }
            return Redirect("/Error/Code/403");
        }

        /// <summary>
        /// Reloan one chosen book
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ActionResult Reloan(int index) 
        {
            Auth auth = new Auth((BorrowerWithUser)Session["User"]);
            if (auth.HasUserPermission()) 
            {
                ActiveAndHistoryBorrows borrows = UserService.GetActiveAndHistoryBorrows(_auth.LoggedInUser.User.PersonId);
                BorrowService.RenewLoan(_auth.LoggedInUser.Borrower, borrows.Active[index].borrow.Barcode);

                return View("Start", borrows);
            }
            return Redirect("/Error/Code/403");
        }
        [HttpGet]
        public ActionResult GetAcountInfo()
        {
            Auth auth = new Auth((BorrowerWithUser)Session["User"]);
            if (auth.HasUserPermission())
                return View(BorrowerService.GetBorrowerWithUserByPersonId(auth.LoggedInUser.User.PersonId));

            return Redirect("/Error/Code/403");
        }
              
        /// <summary>
        /// Updates the logged in users own information
        /// </summary>
        /// <param name="user"></param>
        /// <param name="borrower"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAcountInfo(user user, borrower borrower, string newpassword = null)
        {
            // Maps borrower and user to one object
            BorrowerWithUser borrowerWithUser = new BorrowerWithUser()
            {
                User = user,
                Borrower = borrower
            };

            Auth auth = new Auth((BorrowerWithUser)Session["User"]);

            if (auth.HasUserPermission())
            {
                if (ModelState.IsValid)
                {
                    if (user.Password != null && PasswordService.VerifyPassword(user.Password, auth.LoggedInUser.User.Password))
                    {
                        if (UserService.EmailExists(user.Email) && auth.LoggedInUser.User.Email != user.Email)
                        {
                            borrowerWithUser.PushAlert(AlertView.Build("Email existerar. Försök igen!", AlertType.Danger));
                            return View(borrowerWithUser);
                        }

                        if (!auth.IsSameAs(borrowerWithUser, newpassword))
                        {
                            if (newpassword == "")
                            {
                                UserService.Update(borrowerWithUser, user.Password);
                            }
                            else
                            {
                                if (!PasswordValidaton.IsValid(newpassword))
                                {
                                    borrowerWithUser.PushAlert(AlertView.Build(PasswordValidaton.ErrorMessage, AlertType.Danger));
                                    return View(borrowerWithUser);
                                }

                                UserService.Update(borrowerWithUser, newpassword);

                            }

                            borrowerWithUser.PushAlert(AlertView.Build("Du har uppdaterat ditt konto.", AlertType.Success));
                            Session["User"] = BorrowerService.GetBorrowerWithUserByPersonId(user.PersonId);

                            return View(borrowerWithUser);
                        }
                        else
                        {
                            borrowerWithUser.PushAlert(AlertView.Build("Inget har uppdaterats.", AlertType.Info));
                            return View(borrowerWithUser);
                        }
                    }

                    borrowerWithUser.PushAlert(AlertView.Build("Du måste ange ditt eget lösenord.", AlertType.Danger));
                    return View(borrowerWithUser);
                }

                return View(borrowerWithUser);
            }
            return Redirect("/Error/Code/403");               
        }
	}
}