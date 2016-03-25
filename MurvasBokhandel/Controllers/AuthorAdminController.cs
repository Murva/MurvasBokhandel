using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MurvasBokhandel.Controllers;
using Services.Service;
using Repository.EntityModel;
using Common.Model;
using MurvasBokhandel.Models;
using Common.Share;
using Common;

namespace MurvasBokhandel.Controllers
{
    public class AuthorAdminController : Controller
    {
        // GET: AuthorAdmin
        public ActionResult Start(string letter = "A")
        {
            if (Auth.HasAdminPermission() && LetterLists.LetterList.Contains(letter)) 
                return View(new LettersAndAuthors(LetterLists.LetterList, AuthorService.GetAuthorsByLetter(letter)));
            
            return Redirect("/Error/Code/403");            
        }

        [HttpGet]
        public ActionResult Author(int id)
        {
            if (Auth.HasAdminPermission())
            {
                if (!AuthorService.AuthorExists(id))
                    return Redirect("/Error/Code/404");

                return View(AuthorService.GetAuthorWithBooksAndBooks(id));
            }

            return Redirect("/Error/Code/403");
        }

        [HttpPost]
        public ActionResult Author(AuthorWithBooksAndBooks a)
        {
            if (Auth.HasAdminPermission())
            {
                if (ModelState.IsValid)
                {
                    AuthorService.UpdateAuthor(a.Author);

                    return View(AuthorService.GetAuthorWithBooksAndBooks(a.Author.Aid));
                }

                return View(AuthorService.GetAuthorWithBooksAndBooks(a.Author.Aid));
            }

            return Redirect("/Error/Code/403");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Auth.HasAdminPermission())
            {
                return View();
            }

            return Redirect("/Error/Code/403");
        }

        [HttpPost]
        public ActionResult Create(author a)
        {
            if (Auth.HasAdminPermission()) 
            {
                if (ModelState.IsValid)
                {
                    AuthorService.StoreAuthor(a);

                    return RedirectToAction("Start");
                }

                return View(a);
            }


            return Redirect("/Error/Code/403");
        }

        public ActionResult Remove(AuthorWithBooks a)
        {
            if (Auth.HasAdminPermission())
            {
                AuthorService.DeleteAuthor(a.Author);

                return RedirectToAction("Start");
            }

            return Redirect("/Error/Code/403");
        }

        public ActionResult AddBookToAuthor(int Aid, string ISBN)
        {
            if (Auth.HasAdminPermission())
            {
                if (!BookAuthorService.BookAuthorExists(Aid, ISBN))
                    BookAuthorService.StoreBookAuthor(new bookAuthor()
                    {
                        ISBN = ISBN,
                        Aid = Aid
                    });

                return Redirect("Author/" + Aid);
            }

            return Redirect("/Error/Code/403");
        }

        public ActionResult RemoveBookFromAuthor(int Aid, string ISBN)
        {
            if (Auth.HasAdminPermission())
            {
                BookAuthorService.RemoveBookAuthor(Aid, ISBN);

                return Redirect("Author/" + Aid);
            }

            return Redirect("/Error/Code/403");
        }
    }
}