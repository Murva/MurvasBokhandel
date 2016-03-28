using Common;
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
            if (!AuthorService.AuthorExists(aid))
                return Redirect("/Error/Code/404");

            return View(AuthorService.GetAuthorWithBooks(aid));
        }
    }
}