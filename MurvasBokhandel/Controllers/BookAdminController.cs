using Services.Service;
using System.Linq;
using System.Web.Mvc;
using Common.Model;
using Repository.EntityModel;
using Common.Share;

namespace MurvasBokhandel.Controllers
{
    public class BookAdminController : Controller
    {
        // GET: BookAdmin
        public ActionResult Start(string letter = "A")
        {
            if (Session["Permission"] as string == "Admin" && LetterLists.LetterListWithNum.Contains(letter))
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
            if (Session["Permission"] as string == "Admin")
            {
                return View(BookService.GetBookWithAuthorsAndAuthors(id));
            }
            return Redirect("/Error/Code/403");
        }

        [HttpPost]
        public ActionResult Book(book Book)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (ModelState.IsValid)
                {
                    BookService.UpdateBook(Book);

                    return View(BookService.GetBookWithAuthorsAndAuthors(Book.ISBN));
                }

                return View(BookService.GetBookWithAuthorsAndAuthors(Book.ISBN));
            }

            return Redirect("/Error/Code/403");
        }

        public ActionResult Remove(string isbn)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (!BookService.RemoveBook(isbn)) {
                    TempData["Error"] = "Det gick inte att ta bort bok. Kontrollera att det inte finns knutna lån eller författare.";
                    return Redirect("/BookAdmin/Book/"+isbn);
                }

                return Redirect("/BookAdmin/");
            }
            return Redirect("/Error/Code/403");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Permission"] as string == "Admin")
                return View(BookService.NewBookWithClassifications());

            return Redirect("/Error/Code/403");
        }

        [HttpPost]
        public ActionResult Create(BookWithClassifications bwc, int copies, string library)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (ModelState.IsValid)
                {
                    BookService.StoreBook(bwc.Book, copies, library);

                    return Redirect("/BookAdmin/Book/" + bwc.Book.ISBN);
                }

                return View(BookService.NewBookWithClassifications());
            }

            return Redirect("/Error/Code/403");
        }

        public ActionResult AddAuthorToBook(int Aid, string isbn)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (!BookAuthorService.BookAuthorExists(Aid, isbn))
                    BookAuthorService.StoreBookAuthor(new bookAuthor() { Aid = Aid, ISBN = isbn });

                return RedirectToAction("Book", new { id = isbn });
            }
            return Redirect("/Error/Code/403");
        }

        public ActionResult RemoveAuthorFromBook(string ISBN, int Aid)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BookAuthorService.RemoveBookAuthor(Aid, ISBN);

                return Redirect("/BookAdmin/Book/" + ISBN);
            }
            return Redirect("/Error/Code/403");
        }
    }
}