using System;
using Services.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Model;
using MurvasBokhandel.Controllers;

namespace MurvasBokhandel.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult GetBook(string isbn)
        {
            if (!BookService.BookExists(isbn))
                return Redirect("/Error/Code/404");

            return View(BookService.GetBookWithAuthors(isbn));
        }
    }
}