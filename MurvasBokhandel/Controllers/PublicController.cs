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
        }
    }
}