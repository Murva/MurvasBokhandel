using MurvasBokhandel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{

    public class Book
    {
        public string Name { get; set; }
    };
    public class PublicController : Controller
    {

    //public class BookResult
    //{
    //    public int Aid;
    //    public int ISBN;
    //    public string Title;
    //    public string FirstName;
    //    public string LastName;

    //};

    public class PublicController : Controller
    {
       
        // GET: Public
        public ActionResult Start()
        {
            return View();
        }

        public ActionResult Book(int id)
        {
            List<Book> result = new List<Book>() { new Book() { Name="Harry Potter"} };
            return View(result);
        }

        [HttpGet]
        public ViewResult Search(string publicSearch)
        {

            ViewBag.Result = publicSearch;
            return View();

            //return View(Mockup.Authors.OrderBy(author => author.Aid).ToList());
            //BookWithAuthor bwa = 
            return View(Mockup.BooksWithAuthorResult);

        }
    }
}