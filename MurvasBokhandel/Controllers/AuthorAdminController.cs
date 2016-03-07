using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MurvasBokhandel.Controllers;
using Services.Service;
using Repository.EntityModel;
using Common.Model;

namespace MurvasBokhandel.Controllers
{
    public class AuthorAdminController : Controller
    {
        // GET: AuthorAdmin
        public ActionResult Start(string orderBy = "Aid")
        {
            return View(AuthorService.GetAuthors(orderBy));
        }

        public ActionResult Author(int id)
        {
            if (id <= 0)
                return RedirectToAction("Start");

            return View(AuthorService.GetAuthorWithBooksAndBooks(id));
        }

        public ActionResult Update(AuthorWithBooks a)
        {
            AuthorService.UpdateAuthor(a.Author);

            return Redirect("Author/" + a.Author.Aid);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Store(author a)
        {
            AuthorService.StoreAuthor(a);

            return RedirectToAction("Start");
        }

        public ActionResult Remove(AuthorWithBooks a)
        {
            AuthorService.DeleteAuthor(a.Author);

            return RedirectToAction("Start");
        }

        public ActionResult AddBookToAuthor(int Aid, string ISBN)
        {
            if (!BookAuthorService.BookAuthorExists(Aid, ISBN))
                BookAuthorService.StoreBookAuthor(new bookAuthor()
                {
                    ISBN = ISBN,
                    Aid = Aid
                });

            return Redirect("Author/"+Aid);
        }

        public ActionResult RemoveBookFromAuthor(int Aid, string ISBN)
        {
            BookAuthorService.RemoveBookAuthor(Aid, ISBN);

            return Redirect("Author/"+Aid);
        }
    }
}