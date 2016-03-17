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
            else
            {
                return Redirect("/");
                
            }
        }

        [HttpGet]
        public ActionResult Book(string id)
        {
            if (Session["Permission"] as string == "Admin")
            {
                return View(BookService.GetBookWithAuthorsAndAuthors(id));
            }
            else
            {
                return Redirect("/");
            }
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

            return Redirect("/");
        }

        public ActionResult Remove(string isbn)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BookService.RemoveBook(BookService.GetBook(isbn));

                return Redirect("/BookAdmin/");
            }
            else
            {
                return Redirect("/");
            }
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

            return Redirect("/");

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

            return Redirect("/");
        }

        public ActionResult AddAuthorToBook(int Aid, string isbn)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (!BookAuthorService.BookAuthorExists(Aid, isbn))
                    BookAuthorService.StoreBookAuthor(new bookAuthor() { Aid = Aid, ISBN = isbn });

                return Redirect("/BookAdmin/Book/" + isbn);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult RemoveAuthorFromBook(string ISBN, int Aid)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BookAuthorService.RemoveBookAuthor(Aid, ISBN);

                return Redirect("/BookAdmin/Book/" + ISBN);
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}