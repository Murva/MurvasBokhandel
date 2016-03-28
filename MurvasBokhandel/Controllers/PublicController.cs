using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Service;
using Common.Model;
using System.Web.UI;
using Common.Share;


namespace MurvasBokhandel.Controllers
{
    public class PublicController : Controller
    {
        
        public ActionResult Start()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string search_field)
        {
            return View(AuthorService.GetSearchResult(search_field));
        }

        
        [HttpGet]
        public ActionResult BrowseAuthor(string letter ="A")
        {
            if (LetterLists.LetterList.Contains(letter))
                return View(new LettersAndAuthors(LetterLists.LetterList, AuthorService.GetAuthorsByLetter(letter)));

            return Redirect("/");
        }

        
        [HttpGet]
        public ActionResult BrowseBook(string letter = "A")
        {
            if (LetterLists.LetterListWithNum.Contains(letter))
            {
                if (letter == "123")
                    return View(new LettersAndBooks(LetterLists.LetterListWithNum, BookService.GetBooksByNumber(LetterLists.NumbList)));
                
                return View(new LettersAndBooks(LetterLists.LetterListWithNum, BookService.GetBooksByLetter(letter)));
            }
            return Redirect("/");
        }
    }
}