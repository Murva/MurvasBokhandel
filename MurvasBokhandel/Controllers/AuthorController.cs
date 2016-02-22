using MurvasBokhandel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult GetAuthor(int aid)
        {
            AuthorWithBooks a = (AuthorWithBooks)Mockup.AuthorsWithBooksResults.Where(author => author.Author.Aid == aid).First();
            a.Books = a.Books.OrderBy(books => books.Title).ToList();
            return View(a);
        }
    }
}