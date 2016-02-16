using MurvasBokhandel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    //public class BookResult
    //{
    //    public int Aid;
    //    public int ISBN;
    //    public string Title;
    //    public string FirstName;
    //    public string LastName;

    //};
    public class Book
    {
        public string Name { get; set; }
    };

    public class Author
    {
        public string nameAuthor;
        public string firstLetter;
        int antalBöcker;

    };

    public class PublicController : Controller
    {
       
        // GET: Public
        public ActionResult Start()
        {

            List<List<Author>> Browser = new List<List<Author>>(){
                new List<Author>() { new Author() { nameAuthor = "Alban Dr" }, new Author() { nameAuthor = "Ass Juice" }, new Author() { nameAuthor = "Adams Johanna" }, new Author() { nameAuthor = "Anal Klåda" } },
                new List<Author>() { new Author() { nameAuthor = "Bobbo Krull" } },
                new List<Author>() { new Author() { nameAuthor = "Cpt Kernal" } },
                new List<Author>() { new Author() { nameAuthor = "Kalle Kula" }, new Author() { nameAuthor = "Knark Kungen" } },
                new List<Author>() { new Author() { nameAuthor = "Snoppen" }, new Author() { nameAuthor = "Snippan" } }
            };
            foreach (var author in Browser)
            {

            }

            return View(Mockup.Authors.OrderBy(author => author.LastName).ToList());
            
            //return View();
        }

       

        [HttpGet]
        public ActionResult Search(string search_field)
        {
            //return View(Mockup.Authors.OrderBy(author => author.Aid).ToList());
            //BookWithAuthor bwa = 
            return View(Mockup.BooksWithAuthorResult);
        }
        [HttpGet]
        public ActionResult ClickFörfattare()
        {
            return View(Mockup.Authors.OrderBy(author => author.LastName).ToList());
        }
        [HttpGet]
        public ActionResult ClickBook()
        {
            return View(Mockup.Books.OrderBy(bok => bok.Title).ToList());
        }
    }
}