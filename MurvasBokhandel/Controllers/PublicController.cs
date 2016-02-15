using MurvasBokhandel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    //public class BookResult
    //{
    //    public int Aid;
    //    public int ISBN;
    //    public string Title;
    //    public string FirstName;
    //    public string LastName;

    //};

    public class PublicController : Controller
    {
       
        // GET: Public
        public ActionResult Start()
        {
            
            return View();
        }

       

        [HttpGet]
        public ActionResult Search(string search_field)
        {
            //return View(Mockup.Authors.OrderBy(author => author.Aid).ToList());
            //BookWithAuthor bwa = 
            return View(Mockup.BooksWithAuthorResult);
        }
    }
}