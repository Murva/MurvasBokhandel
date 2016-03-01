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
            return View(BookService.GetBooks());
        }

        public ActionResult Book(string id)
        {
            return View(BookService.GetBook(id));
        }

        public ActionResult Update(Repository.EntityModel.book Book)
        {
            BookService.UpdateBook(Book);

            return Redirect("/BookAdmin/Book/"+Book.ISBN);
        }

        public ActionResult Remove(Mockup.BOOK Book)
        {
            //OBS! Ska tas bort från tabellen "BOOK" och "BOOK_AUTHOR"
            Mockup.Books.Remove(Mockup.Books.Where(b => b.ISBN == Book.ISBN).First());
            Mockup.Book_Authors.Remove(Mockup.Book_Authors.Where(b => b.ISBN == Book.ISBN).First());

            //Används till mockup
            //foreach (AuthorWithBooks a in Mockup.AuthorsWithBooksResults)
            //    a.Books.Remove(Book);

            return Redirect("/BookAdmin/");
        }

        public ActionResult Create()
        {
            return View(new BookWithClassifications()
            {
                Book = new Repository.EntityModel.book(),
                Classifications = ClassificationService.GetClassifications()
            });
        }

        public ActionResult Store(BookWithClassifications bwc)
        {
            BookService.StoreBook(bwc.Book);

            return Redirect("/BookAdmin/");
        }
    }
}