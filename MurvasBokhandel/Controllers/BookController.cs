using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    public class Book
    {
        public int ISBN { get; set; }
        public string Title { get; set; }
        public int SignId { get; set; }
        public int PublicationYear { get; set; }
        public string Publicationinfo { get; set; }
        public int Pages { get; set; }
    }
    public class BookController : Controller
    {
        static List<Book> Books = new List<Book>()
        {
            new Book()
            {
                ISBN = 11111,
                Title = "Harry Potter och fången från Azkaban",
                SignId = 1,
                PublicationYear = 1974,
                Publicationinfo = "Murvas förlag",
                Pages = 345
            },

            new Book()
            {
                ISBN = 22222,
                Title = "Harry Pulver och Kalle",
                SignId = 2,
                PublicationYear = 1975,
                Publicationinfo = "Murvas förlag",
                Pages = 545
            },

            new Book()
            {
                ISBN = 33333333,
                Title = "En murvig Murva",
                SignId = 3,
                PublicationYear = 1976,
                Publicationinfo = "Murvas förlag",
                Pages = 595
            },


        };
        // GET: Book
        public ActionResult GetBook(int isbn)
        {
            return View(Books.Where(book => book.ISBN == isbn).First());
        }
    }
}