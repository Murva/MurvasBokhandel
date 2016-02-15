using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MurvasBokhandel.Models
{
    public class BookWithAuthor
    {
        //public Mockup.BOOK_AUTHOR Book_Author { get; set; }
        public Mockup.BOOK Book { get; set; }
        public Mockup.AUTHOR Author { get; set; }
    }
}