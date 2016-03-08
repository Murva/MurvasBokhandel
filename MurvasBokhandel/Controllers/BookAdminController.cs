using MurvasBokhandel.Models;
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

        public ActionResult Update(book Book)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BookService.UpdateBook(Book);

                return Redirect("/BookAdmin/Book/" + Book.ISBN);
            }
            else
            {
                return Redirect("/");
            }
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
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Store(BookWithClassifications bwc)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BookService.StoreBook(bwc.Book);

                return Redirect("/BookAdmin/");
            }
            else
            {
                return Redirect("/");
            }
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