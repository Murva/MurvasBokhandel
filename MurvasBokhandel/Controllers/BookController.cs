using System;
using Services.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MurvasBokhandel.Models;
using MurvasBokhandel.Controllers;

namespace MurvasBokhandel.Controllers
{
    
    public class BookController : Controller
    {
        
        //};
        // GET: Book
        public ActionResult GetBook(string isbn)
        {
            //BookWithAuthor bwa = (BookWithAuthor)Mockup.BooksWithAuthorResult.Where(book => book.Book.ISBN == isbn).First();
            BookAndAuthors baa = AuthorService.GetAuthors(isbn)
            return View(bwa);
        }
    }
}