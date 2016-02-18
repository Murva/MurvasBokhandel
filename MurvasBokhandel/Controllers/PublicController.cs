using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    public class BookResult
    {
        public int Author_Id;
        public int Book_Id;
        public string Book_name;
        public string Author;
    };

    public class PublicController : Controller
    {
        List<BookResult> SearchResults = new List<BookResult>() {
            new BookResult() {
                Author_Id = 1,
                Book_Id = 222,
                Book_name = "Harry Potter och fången från Azkaban",
                Author = "J.K Rowling"
            },
            new BookResult() {
                Author_Id = 1,
                Book_Id = 223,
                Book_name = "Harry Potter och Hemligheternas kammare",
                Author = "J.K Rowling"
            }
        };

        public ActionResult Start()
        {
            return View();
        }

        public ActionResult Book(int id)
        {
            return View();
        }

        [HttpGet]
        public ViewResult Search(string publicSearch)
        {
            return View(SearchResults);
        }
    }
}