using Common;
using Common.Model;
using Common.Share;
using Repository.EntityModel;
using Services.Service;
using System.Linq;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    public class BookAdminController : Controller
    {
        // GET: BookAdmin
        public ActionResult Start(string letter = "A")
        {
            if (Auth.HasAdminPermission() && LetterLists.LetterListWithNum.Contains(letter))
            {
                if (letter != "123")
                    return View(new LettersAndBooks(LetterLists.LetterListWithNum, BookService.GetBooksByLetter(letter)));
                else
                    return View(new LettersAndBooks(LetterLists.LetterListWithNum, BookService.GetBooksByNumber(LetterLists.NumbList)));
            }
            
            return Redirect("/Error/Code/403");
        }

        [HttpGet]
        public ActionResult Book(string id)
        {
            if (Auth.HasAdminPermission())
                return View(BookService.GetBookWithAuthorsAndAuthors(id));
            
            return Redirect("/Error/Code/403");
        }

        [HttpPost]
        public ActionResult Book(book Book)
        {
            if (Auth.HasAdminPermission())
            {
                if (ModelState.IsValid)
                {
                    BookService.UpdateBook(Book);

                    Auth.PushAlert(AlertView.Build("Du har uppdaterat bokens uppgifter", AlertType.Success));

                    return View(BookService.GetBookWithAuthorsAndAuthors(Book.ISBN));
                }

                Auth.PushAlert(AlertView.BuildErrors(ViewData));

                return View(BookService.GetBookWithAuthorsAndAuthors(Book.ISBN));
            }

            return Redirect("/Error/Code/403");
        }

        public ActionResult Remove(string isbn)
        {
            if (Auth.HasAdminPermission())
            {
                if (!BookService.RemoveBook(isbn)) {

                    Auth.PushAlert(AlertView.Build("Det gick inte att ta bort bok. Kontrollera att det inte finns knutna lån eller författare.", AlertType.Danger));

                    return RedirectToAction("Book", new { id = isbn });
                }

                Auth.PushAlert(AlertView.Build("Boken med ISBN "+isbn+" är nu borttagen.", AlertType.Success));

                return RedirectToAction("Start");
            }

            return Redirect("/Error/Code/403");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Auth.HasAdminPermission())
                return View(BookService.NewBookWithClassifications());

            return Redirect("/Error/Code/403");
        }

        [HttpPost]
        public ActionResult Create(BookWithClassifications bwc, int copies, string library)
        {
            if (Auth.HasAdminPermission())
            {
                if (ModelState.IsValid)
                {
                    BookService.StoreBook(bwc.Book, copies, library);

                    return RedirectToAction("Book", new { id = bwc.Book.ISBN });
                }

                ViewBag.Alert = AlertView.BuildErrors(ViewData);

                return View(BookService.NewBookWithClassifications());
            }

            return Redirect("/Error/Code/403");
        }

        public ActionResult AddAuthorToBook(int Aid, string isbn)
        {
            if (Auth.HasAdminPermission())
            {
                if (!BookAuthorService.BookAuthorExists(Aid, isbn))
                    BookAuthorService.StoreBookAuthor(new bookAuthor() { Aid = Aid, ISBN = isbn });

                return RedirectToAction("Book", new { id = isbn });
            }
            return Redirect("/Error/Code/403");
        }

        public ActionResult RemoveAuthorFromBook(string ISBN, int Aid)
        {
            if (Auth.HasAdminPermission())
            {
                BookAuthorService.RemoveBookAuthor(Aid, ISBN);

                return Redirect("/BookAdmin/Book/" + ISBN);
            }
            return Redirect("/Error/Code/403");
        }
    }
}