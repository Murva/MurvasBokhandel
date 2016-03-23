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
            if (Session["Permission"] as string != null)
            {
                BorrowerWithUser u = (BorrowerWithUser)Session["User"];
                BBC = BorrowService.GetBorrowedBooks(u.User.PersonId);
                return View(BBC);
            }
            return Redirect("/");
        }

        // Lånar om de böcker som är möjliga att låna om
        public ActionResult ReloanAll() {
            if (Session["Permission"] as string != null)
            {
                foreach (BorrowedBookCopy b in BBC)
                {
                    if (b.fine == 0)
                    {
                        BorrowService.updateBorrowDate(b.borrow);
                        BorrowService.updateToBeReturnedDate(b.borrow, b.category.Period);
                    }
                }
                return RedirectToAction("Start", BBC);
            }
            return Redirect("/");
        }

        // Lånar om enskild bok
        public ActionResult Reloan(int index) 
        {
            if (Session["Permission"] as string != null) 
            {
                BorrowService.updateBorrowDate(BBC[index].borrow);
                BorrowService.updateToBeReturnedDate(BBC[index].borrow, BBC[index].category.Period);
                return View("Start", BBC);
            }
            return Redirect("/");
        }
        [HttpGet]
        public ActionResult GetAcountInfo()
        {
            if (Session["Permission"] as string != null) {
                BorrowerWithUser user = (BorrowerWithUser)Session["User"];        
                BorrowerWithUser activeUser = BorrowerService.GetBorrowerWithUserByPersonId(user.User.PersonId);
            
                //BorrowerWithUser activeUser = new BorrowerWithUser();
                return View(activeUser);
            }
            return Redirect("/");
        }
              
        [HttpPost]
        public ActionResult GetAcountInfo(user user, borrower borrower)//user user, borrower borrower
        {
            //borrower.PersonId = user.PersonId;
            if (Session["Permission"] as string != null)
            {
                if (ModelState.IsValid) 
                {

                    Repository.EntityModel.user activeUser = (Repository.EntityModel.user)Session["User"];

                    if (Services.Service.UserService.emailExists(user.Email) && (!(activeUser.Email == user.Email)))
                    {
                        ViewBag.Error = "Epostadressen finns redan registrerad."; // denna går inte just nu!!!!!                        
                        BorrowerWithUser someOneElseEmail = BorrowerService.GetBorrowerWithUserByPersonId(activeUser.PersonId);
                        return View(someOneElseEmail);


                        //return View(activeUser);     //Skicka tillbaka att det är en upptagen adress           
                        
                    }

                    BorrowerWithUser borrowerWithUser = new BorrowerWithUser();
                    borrowerWithUser.User = user;
                    borrowerWithUser.Borrower = borrower;
                    borrowerWithUser.Borrower.PersonId = user.PersonId;
                    UserService.update(borrowerWithUser);
                    Session["User"] = AuthService.GetUserByPersonId(user.PersonId);//Denna måste nog ändras

                    // + user.Borrower.PersonId
                    return Redirect("/User/GetAcountInfo/");
                }
                else
                {
                    BorrowerWithUser original = (BorrowerWithUser)Session["User"];

                    return View(BorrowerService.GetBorrowerWithUserByPersonId(original.User.PersonId));
                }
            }
            return View();               
        }
	}
}