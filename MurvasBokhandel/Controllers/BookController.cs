using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    //public class Book
    //{
    //    public int ISBN { get; set; }
    //    public string Title { get; set; }
    //    public int SignId { get; set; }
    //    public int PublicationYear { get; set; }
    //    public string Publicationinfo { get; set; }
    //    public int Pages { get; set; }
    //}
    public class BookController : Controller
    {
        
        //};
        // GET: Book
        public ActionResult GetBook(int isbn)
        {
            return View(/*Books.Where(book => book.ISBN == isbn).First()*/);
        }
    }
}