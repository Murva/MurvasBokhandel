using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MurvasBokhandel.Models;
using MurvasBokhandel.Controllers;

namespace MurvasBokhandel.Controllers
{
    public class AuthorAdminController : Controller
    {
        // GET: AuthorAdmin
        public ActionResult Start(string orderBy = "id")
        {
            if (orderBy == "id")
                return View(Mockup.Authors.OrderBy(author => author.Aid).ToList());
            else if (orderBy == "name")
                return View(Mockup.Authors.OrderBy(author => author.FirstName).ToList());
            else
                return View(Mockup.Authors.OrderBy(author => author.BirthYear).ToList());
        }

        public ActionResult Author(int id = -1)
        {
            if (id == -1)
                return RedirectToAction("Start");

            AuthorWithBooks a = (AuthorWithBooks)Mockup.AuthorsWithBooksResults.Where(author => author.Author.Aid == id).First();

            if (TempData.Count != 0)
            {
                ViewBag.Alert = TempData["Alert"].ToString();
                ViewBag.Status = TempData["Status"].ToString();
                TempData.Remove("Alert");
                TempData.Remove("Status");
            }

            return View(a);
        }

        public ActionResult Update(AuthorWithBooks a)
        {
            AuthorWithBooks ar = (AuthorWithBooks) Mockup.AuthorsWithBooksResults.Where(author => author.Author.Aid == a.Author.Aid).First();
            ar.Author.FirstName = a.Author.FirstName;
            ar.Author.LastName = a.Author.LastName;

            TempData["Alert"] = "Författaren är uppdaterad";
            TempData["Status"] = "success";

            return Redirect("Author/" + a.Author.Aid);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Store(Mockup.AUTHOR a)
        {
            a.Aid = Mockup.AuthorsWithBooksResults.OrderByDescending(author => author.Author.Aid).First().Author.Aid + 1;
            AuthorWithBooks ar = new AuthorWithBooks()
            {
                Author = a,
                Books = new List<Mockup.BOOK>()
            };
            Mockup.Authors.Add(a);
            Mockup.AuthorsWithBooksResults.Add(ar);

            return RedirectToAction("Start");
        }

        public ActionResult Remove(AuthorWithBooks a)
        {
            Mockup.AuthorsWithBooksResults.Remove(Mockup.AuthorsWithBooksResults.Where(author => author.Author.Aid == a.Author.Aid).ElementAt(0));

            /*
             foreach author.books, remove
             */

            return RedirectToAction("Start");
        }

        public ActionResult AddBookToAuthor(int Aid, int ISBN, int Publicationyear, string Title, string Publicationinfo, string Pages)
        {
            Mockup.BOOK book = new Mockup.BOOK()
            {
                ISBN = ISBN,
                Publicationinfo = Publicationinfo,
                PublicationYear = Publicationyear,
                Title = Title,
                Pages = Pages,
                SignId = 0
            };

            Mockup.AuthorsWithBooksResults.Where(author => author.Author.Aid == Aid).First().Books.Add(book);

            return Redirect("Author/"+Aid);
        }
    }
}