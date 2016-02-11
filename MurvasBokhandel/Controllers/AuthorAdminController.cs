using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MurvasBokhandel.Controllers;

namespace MurvasBokhandel.Controllers
{
    public class Book
    {
        public long ISBN;
        public string Title;
    }

    public class AuthorResult
    {
        public int Aid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Book> Books { get; set; }
    }

    public class AuthorAdminController : Controller
    {
        static List<AuthorResult> AuthorsResults = new List<AuthorResult>() {
            new AuthorResult() {
                Aid = 1,
                FirstName = "J.K",
                LastName = "Rowling",
                Books = new List<Book>() {
                    new Book() { ISBN=9789129697704, Title="Harry Potter och De vises sten"},
                    new Book() { ISBN=9789129675559, Title="Harry Potter och hemligheternas kammare"},
                    new Book() { ISBN=9789129675566, Title="Harry Potter och fången från Azkaban"}
                }
            },
            new AuthorResult() {
                Aid = 2,
                FirstName = "Liza",
                LastName = "Marklund",
                Books = new List<Book>() {
                    new Book() {ISBN=9789164204530, Title="Järnblod"},
                    new Book() {ISBN=9789175790336, Title="Lyckliga gatan"}
                }
            },
            new AuthorResult() {
                Aid = 3,
                FirstName = "Astrid",
                LastName = "Lindgren",
                Books = new List<Book>() {
                    new Book() {ISBN=9789129697308, Title="Allrakäraste syster"},
                    new Book() {ISBN=9789129698442, Title="Känner du Pippi Långstrump?"}
                }
            }
        };

        // GET: AuthorAdmin
        public ActionResult Start()
        {
            return View(AuthorsResults);
        }

        public ActionResult Author(int id)
        {
            if (id == 0)
                return RedirectToAction("Start");

            AuthorResult a = (AuthorResult)AuthorsResults.Where(author => author.Aid == id).ElementAt(0);

            if (TempData.Count != 0)
            {
                ViewBag.Alert = TempData["Alert"].ToString();
                ViewBag.Status = TempData["Status"].ToString();
                TempData.Remove("Alert");
                TempData.Remove("Status");
            }

            return View(a);
        }

        public ActionResult Update(AuthorResult a)
        {
            AuthorResult ar = (AuthorResult)AuthorsResults.Where(author => author.Aid == a.Aid).ElementAt(0);
            ar.FirstName = a.FirstName;
            ar.LastName = a.LastName;

            TempData["Alert"] = "Författaren är uppdaterad";
            TempData["Status"] = "success";

            return RedirectToAction("Author", a);
        }

        public ActionResult Remove(AuthorResult a)
        {
            AuthorsResults.Remove(AuthorsResults.Where(author => author.Aid == a.Aid).ElementAt(0));

            /*
             foreach author.books, remove
             */

            return RedirectToAction("Start");
        }
    }
}