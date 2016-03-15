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

namespace MurvasBokhandel.Controllers
{
    public class AuthorAdminController : Controller
    {
        // GET: AuthorAdmin
        public ActionResult Start(string orderBy = "Aid")
        {
            if (Session["Permission"] as string == "Admin") 
            {
                return View(AuthorService.GetAuthors(orderBy));
            }
            else
            {
                return Redirect("/");
            }
            
        }

        public ActionResult Author(int id)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (id <= 0)
                    return RedirectToAction("Start");

                return View(AuthorService.GetAuthorWithBooksAndBooks(id));
            }
            else 
            {
                return Redirect("/");
            }
                
        }

        public ActionResult Update(AuthorWithBooks a)
        {
            if (Session["Permission"] as string == "Admin")
            {
                AuthorService.UpdateAuthor(a.Author);

                return Redirect("Author/" + a.Author.Aid);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Permission"] as string == "Admin")
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public ActionResult Create(author a)
        {
            if (Session["Permission"] as string == "Admin" && ModelState.IsValid)
            {
                //AuthorService.StoreAuthor(a);

                return RedirectToAction("Start");
            }

            return View(a);
        }

        public ActionResult Remove(AuthorWithBooks a)
        {
            if (Session["Permission"] as string == "Admin")
            {
                AuthorService.DeleteAuthor(a.Author);

                return RedirectToAction("Start");
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult AddBookToAuthor(int Aid, string ISBN)
        {
            if (Session["Permission"] as string == "Admin")
            {
                if (!BookAuthorService.BookAuthorExists(Aid, ISBN))
                    BookAuthorService.StoreBookAuthor(new bookAuthor()
                    {
                        ISBN = ISBN,
                        Aid = Aid
                    });

                return Redirect("Author/" + Aid);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult RemoveBookFromAuthor(int Aid, string ISBN)
        {
            if (Session["Permission"] as string == "Admin")
            {
                BookAuthorService.RemoveBookAuthor(Aid, ISBN);

                return Redirect("Author/" + Aid);
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}