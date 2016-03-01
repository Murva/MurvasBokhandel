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

        public ActionResult Remove(book Book)
        {
            BookService.RemoveBook(book);

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