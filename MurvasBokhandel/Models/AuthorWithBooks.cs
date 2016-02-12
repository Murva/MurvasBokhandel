using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MurvasBokhandel.Models
{
    public class AuthorWithBooks
    {
        public Mockup.AUTHOR Author { get; set; }
        public List<Mockup.BOOK> Books { get; set; }
    }
}