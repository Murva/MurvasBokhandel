using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Service;
using Common.Model;

namespace MurvasBokhandel.Controllers
{
    public class PublicController : Controller
    {
       
        
        public ActionResult Start()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string search_field)
        {
            //return View(Mockup.Authors.OrderBy(author => author.Aid).ToList());
            //BookWithAuthor bwa = 
            //return View(Mockup.BooksWithAuthorResult.OrderBy(author => author.Author.FirstName).ToList());
            AuthorsAndBooks a = AuthorService.GetSearchResult(search_field);
            
            return View(a);
        }

        [HttpGet]
        public ActionResult BrowseAuthor()
        {
            //return View(Mockup.Authors.OrderBy(author => author.LastName).ToList());
            return View(AuthorService.GetAuthors("LastName"));
            //LastName
        }

        [HttpGet]
        public ActionResult BrowseBook()
        {
            //return View(Mockup.Books.OrderBy(book => book.Title).ToList());
            
            return View(BookService.GetBooks().OrderBy(book => book.Title).ToList());
        }
    }
}