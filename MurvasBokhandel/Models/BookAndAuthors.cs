using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MurvasBokhandel.Models
{
    public class BookAndAuthors
    {
        public Mockup.BOOK Book { get; set; }
        public List<Mockup.AUTHOR> Authors { get; set; }
        public int Aid { get; set; }
    }
}