using Common.Model;
using Common;
using Common.Share;
using Repository.EntityModel;
using System.Web.Mvc;
using Services.Service;


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
            //Knyter samman user och borrower -objekten
            BorrowerWithUser borrowerWithUser = new BorrowerWithUser()
            {
                User = user,
                Borrower = borrower
            };

            if (Auth.HasUserPermission())
            {
                if (ModelState.IsValid)
                {
                    if (PasswordService.VerifyPassword(user.Password, Auth.LoggedInUser.User.Password))
                    {
                        if (UserService.EmailExists(user.Email) && Auth.LoggedInUser.User.Email != user.Email)
                        {
                            borrowerWithUser.PushAlert(AlertView.Build("Email existerar. Försök igen!", AlertType.Danger));
                            return View(borrowerWithUser);
                        }

                        if (newpassword == "")
                            UserService.Update(borrowerWithUser, user.Password);
                        else
                            UserService.Update(borrowerWithUser, newpassword);

                        borrowerWithUser.PushAlert(AlertView.Build("Du har uppdaterat ditt konto.", AlertType.Success));
                        Auth.UpdateUser(BorrowerService.GetBorrowerWithUserByPersonId(user.PersonId));

                        return View(borrowerWithUser);
                    }

                    borrowerWithUser.PushAlert(AlertView.Build("Du måste ange ditt lösenord.", AlertType.Danger));
                    return View(borrowerWithUser);
                }

                return View(borrowerWithUser);
            }
            return Redirect("/Error/Code/403");               
        }
	}
}