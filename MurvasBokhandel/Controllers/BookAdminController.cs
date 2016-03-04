﻿using MurvasBokhandel.Models;
using Services.Service;
using System.Linq;
using System.Web.Mvc;
using Common.Model;
using Repository.EntityModel;

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
            return View(BookService.GetBookWithAuthorsAndAuthors(id));
        }

        public ActionResult Update(book Book)
        {
            BookService.UpdateBook(Book);

            return Redirect("/BookAdmin/Book/"+Book.ISBN);
        }

        public ActionResult Remove(string isbn)
        {
            BookService.RemoveBook(BookService.GetBook(isbn));

            return Redirect("/BookAdmin/");
        }

        public ActionResult Create()
        {
            return View(new BookWithClassifications()
            {
                Book = new Repository.EntityModel.book(),
                Classifications = ClassificationService.GetClassifications()
            });
        }

        public ActionResult Store(BookWithClassifications bwc)
        {
            BookService.StoreBook(bwc.Book);

            return Redirect("/BookAdmin/");
        }

        public ActionResult AddAuthorToBook(int Aid, string isbn)
        {
            if (!BookAuthorService.BookAuthorExists(Aid, isbn))
                BookAuthorService.StoreBookAuthor(new bookAuthor() { Aid = Aid, ISBN = isbn });

            return Redirect("/BookAdmin/Book/"+isbn);
        }

        public ActionResult RemoveAuthorFromBook(string ISBN, int Aid)
        {
            BookAuthorService.RemoveBookAuthor(Aid, ISBN);

            return Redirect("/BookAdmin/Book/"+ISBN);
        }
    }
}