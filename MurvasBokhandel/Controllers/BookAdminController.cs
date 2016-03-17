using Services.Service;
using System.Linq;
using System.Web.Mvc;
using Common.Model;
using Repository.EntityModel;

namespace MurvasBokhandel.Controllers
{
    public class BookAdminController : Controller
    {
        // GET: BookAdmin
        public ActionResult Start()
        {
            if (Session["Permission"] as string == "Admin")
            {
                return View(BookService.GetBooks());
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
                BookService.RemoveBook(BookService.GetBook(isbn));

                return Redirect("/BookAdmin/");
            }
            return Redirect("/Error/Code/403");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Permission"] as string == "Admin")
            {
                return View(new BookWithClassifications()
                {
                    Book = new Repository.EntityModel.book(),
                    Classifications = ClassificationService.GetClassifications()
                });
            }

            return Redirect("/Error/Code/403");
        }

        [HttpPost]
        public ActionResult Create(BookWithClassifications bwc)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (ModelState.IsValid)
                {
                    BookService.StoreBook(bwc.Book);

                    return Redirect("/BookAdmin/");
                }

                return View(new BookWithClassifications()
                {
                    Book = new Repository.EntityModel.book(),
                    Classifications = ClassificationService.GetClassifications()
                });
            }

            return Redirect("/Error/Code/403");
        }

        public ActionResult AddAuthorToBook(int Aid, string isbn)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (!BookAuthorService.BookAuthorExists(Aid, isbn))
                    BookAuthorService.StoreBookAuthor(new bookAuthor() { Aid = Aid, ISBN = isbn });

                return Redirect("/BookAdmin/Book/" + isbn);
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