using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MurvasBokhandel.Models;
using MurvasBokhandel.Controllers;

namespace MurvasBokhandel.Controllers
{
    public class AuthorResult
    {
        public Mockup.AUTHOR Author { get; set; }
        public List<Mockup.BOOK> Books { get; set; }
    }

    public class AuthorAdminController : Controller
    {
        static List<Mockup.AUTHOR> Authors = new List<Mockup.AUTHOR>()
        {
            new Mockup.AUTHOR() {
                Aid = 1,
                FirstName = "J.K",
                LastName = "Rowling",
                BirthYear = 1961
            },
            new Mockup.AUTHOR() {
                Aid = 2,
                FirstName = "Liza",
                LastName = "Marklund",
                BirthYear = 1967
            },
            new Mockup.AUTHOR() {
                Aid = 3,
                FirstName = "Astrid",
                LastName = "Lindgren",
                BirthYear = 1917
            }
        };

        static List<Mockup.BOOK_AUTHOR> Book_Authers = new List<Mockup.BOOK_AUTHOR>() {
            new Mockup.BOOK_AUTHOR() { Aid = 1, ISBN = 9789129697704 },
            new Mockup.BOOK_AUTHOR() { Aid = 1, ISBN = 9789129675559 },
            new Mockup.BOOK_AUTHOR() { Aid = 1, ISBN = 9789129675566 },
            new Mockup.BOOK_AUTHOR() { Aid = 2, ISBN = 9789164204530 },
            new Mockup.BOOK_AUTHOR() { Aid = 2, ISBN = 9789175790336 },
            new Mockup.BOOK_AUTHOR() { Aid = 3, ISBN = 9789129697308 },
            new Mockup.BOOK_AUTHOR() { Aid = 4, ISBN = 9789129698442 },
        };

        static List<Mockup.BOOK> Books = new List<Mockup.BOOK>()
        {
            new Mockup.BOOK() { ISBN=9789129697704, Title="Harry Potter och De vises sten", SignId = 0, Pages = "512", PublicationYear = 1997, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789129675559, Title="Harry Potter och hemligheternas kammare", SignId = 0, Pages = "751", PublicationYear = 1999, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789129675566, Title="Harry Potter och fången från Azkaban", SignId = 0, Pages = "895", PublicationYear = 2001, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789164204530, Title="Järnblod", SignId = 0, Pages = "523", PublicationYear = 2011, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789175790336, Title="Lyckliga gatan", SignId = 0, Pages = "566", PublicationYear = 2012, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789129697308, Title="Allrakäraste syster", SignId = 0, Pages = "876", PublicationYear = 1975, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789129698442, Title="Känner du Pippi Långstrump?", SignId = 0, Pages = "120", PublicationYear = 1961, Publicationinfo = "No info"}
        };

        static List<AuthorResult> AuthorsWithBooksResults = new List<AuthorResult>() {
            new AuthorResult() {
                Author = Authors[0],
                Books = new List<Mockup.BOOK>() {
                    Books[0],
                    Books[1],
                    Books[2]
                }
            },
            new AuthorResult() {
                Author = Authors[1],
                Books = new List<Mockup.BOOK>() {
                    Books[3],
                    Books[4]
                }
            },
            new AuthorResult() {
                Author = Authors[2],
                Books = new List<Mockup.BOOK>() {
                    Books[5],
                    Books[6]
                } 
            }
        };

        // GET: AuthorAdmin
        public ActionResult Start(string orderBy = "id")
        {
            if (orderBy == "id")
                return View(Authors.OrderBy(author => author.Aid).ToList());
            else if (orderBy == "name")
                return View(Authors.OrderBy(author => author.FirstName).ToList());
            else
                return View(Authors.OrderBy(author => author.BirthYear).ToList());
        }

        public ActionResult Author(int id)
        {
            if (id == 0)
                return RedirectToAction("Start");

            AuthorResult a = (AuthorResult)AuthorsWithBooksResults.Where(author => author.Author.Aid == id).First();

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
            AuthorResult ar = (AuthorResult)AuthorsWithBooksResults.Where(author => author.Author.Aid == a.Author.Aid).First();
            ar.Author.FirstName = a.Author.FirstName;
            ar.Author.LastName = a.Author.LastName;

            TempData["Alert"] = "Författaren är uppdaterad";
            TempData["Status"] = "success";

            return Redirect("Author/" + a.Author.Aid);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Store(Mockup.AUTHOR a)
        {
            a.Aid = AuthorsWithBooksResults.OrderByDescending(author => author.Author.Aid).First().Author.Aid + 1;
            AuthorResult ar = new AuthorResult()
            {
                Author = a,
                Books = new List<Mockup.BOOK>()
            };
            Authors.Add(a);
            AuthorsWithBooksResults.Add(ar);

            return RedirectToAction("Start");
        }

        public ActionResult Remove(AuthorResult a)
        {
            AuthorsWithBooksResults.Remove(AuthorsWithBooksResults.Where(author => author.Author.Aid == a.Author.Aid).ElementAt(0));

            /*
             foreach author.books, remove
             */

            return RedirectToAction("Start");
        }

        public ActionResult AddBookToAuthor(int Aid, int ISBN, int Publicationyear, string Title, string Publicationinfo, string Pages)
        {
            Mockup.BOOK book = new Mockup.BOOK()
            {
                ISBN = ISBN,
                Publicationinfo = Publicationinfo,
                PublicationYear = Publicationyear,
                Title = Title,
                Pages = Pages,
                SignId = 0
            };

            AuthorsWithBooksResults.Where(author => author.Author.Aid == Aid).First().Books.Add(book);

            return Redirect("Author/"+Aid);
        }
    }
}