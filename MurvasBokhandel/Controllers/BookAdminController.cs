using MurvasBokhandel.Models;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    public class BookAdminController : Controller
    {
        // GET: BookAdmin
        public ActionResult Start()
        {
            return View(BookService.GetBooks());
        }

        public ActionResult Book(string id)
        {
            return View(BookService.GetBook(id));
        }

        public ActionResult Update(Repository.EntityModel.book Book)
        {
            BookService.UpdateBook(Book);

            return Redirect("/BookAdmin/Book/"+Book.ISBN);
        }

        public ActionResult Remove(Mockup.BOOK Book)
        {
            //OBS! Ska tas bort från tabellen "BOOK" och "BOOK_AUTHOR"
            Mockup.Books.Remove(Mockup.Books.Where(b => b.ISBN == Book.ISBN).First());
            Mockup.Book_Authors.Remove(Mockup.Book_Authors.Where(b => b.ISBN == Book.ISBN).First());

            //Används till mockup
            //foreach (AuthorWithBooks a in Mockup.AuthorsWithBooksResults)
            //    a.Books.Remove(Book);

            return Redirect("/BookAdmin/");
        }

        public ActionResult Create()
        {
            return View(new BookAndAuthors()
            {
                Book = new Mockup.BOOK(),
                Authors = Mockup.Authors
            });
        }

        public ActionResult Store(BookAndAuthors b)
        {
            AuthorWithBooks awb = Mockup.AuthorsWithBooksResults.Where(author => author.Author.Aid == b.Aid).First();
            
            //OBS! Ska adderas till tabellen "BOOK" och "BOOK_AUTHOR" i databasen
            awb.Books.Add(b.Book);
            Mockup.Books.Add(b.Book);
            Mockup.Book_Authors.Add(new Mockup.BOOK_AUTHOR() { Aid = b.Aid, ISBN = b.Book.ISBN });

            return Redirect("/BookAdmin/");
        }
    }
}