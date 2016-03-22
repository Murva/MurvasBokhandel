using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Service;
using Common.Model;
using System.Web.UI;


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
            
            AuthorsAndBooks a = AuthorService.GetSearchResult(search_field);
            
            return View(a);
        }

        
        [HttpGet]
        public ActionResult BrowseAuthor(string letter ="A")
        {
            List<string> letterList = new List<string>(new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "Å", "Ä", "Ö"});
            foreach (string l in letterList)
            {
                if (letter == l)
                {
                    LettersAndAuthors la = new LettersAndAuthors()
                    {
                        Letters = letterList,
                        Authors = AuthorService.GetAuthorsByLetter(letter)
                    };

                    return View(la);
                }
                
            }
            return Redirect("/");                             
            
        }

        
        [HttpGet]
        public ActionResult BrowseBook(string letter = "A")
        {
            List<string> letterList = new List<string>(new string[] { "123", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "Å", "Ä", "Ö" });
            foreach (string l in letterList)
            {
                if (letter == l)
                {
                    if (letter == "123")
                    {
                        List<string> numb = new List<string>(new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" });
                        LettersAndBooks lb = new LettersAndBooks()
                        {
                            Books = BookService.GetBooksByNumber(numb),
                            Letters = letterList
                        };
                        return View(lb);
                    }
                    else
                    {
                        LettersAndBooks lb = new LettersAndBooks()
                        {
                            Books = BookService.GetBooksByLetter(letter),
                            Letters = letterList
                        };
                        return View(lb);
                    }
                }
            }
            return Redirect("/");
        }
    }
}