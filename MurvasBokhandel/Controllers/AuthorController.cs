using Common.Model;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult GetAuthor(int aid)
        {
            return View(AuthorService.GetAuthorWithBooks(aid));
        }
    }
}