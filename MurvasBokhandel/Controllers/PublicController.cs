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
            
            AuthorsAndBooks a = AuthorService.GetSearchResult(search_field);
            
            return View(a);
        }

        [HttpGet]
        public ActionResult BrowseAuthor()
        {
            
                return View(AuthorService.GetAuthors("LastName"));
                            
            
        }

        [HttpGet]
        public ActionResult BrowseBook()
        {
                       
            return View(BookService.GetBooks().OrderBy(book => book.Title).ToList());
        }
    }
}