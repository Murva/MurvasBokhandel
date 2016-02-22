using MurvasBokhandel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{

    public class PublicController : Controller
    {
       
        // GET: Public
        public ActionResult Start()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Search(string publicSearch)
        {
            //return View(Mockup.Authors.OrderBy(author => author.Aid).ToList());
            //BookWithAuthor bwa = 
            return View(Mockup.BooksWithAuthorResult.OrderBy(author => author.Author.FirstName).ToList());
        }
    }
}