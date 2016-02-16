using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MurvasBokhandel.Models
{
    public class BorrowedBookCopy
    {
        public MurvasBokhandel.Models.Mockup.BOOK book { get; set; }
        public MurvasBokhandel.Models.Mockup.COPY copy { get; set; }
        public MurvasBokhandel.Models.Mockup.STATUS status { get; set; }
        public MurvasBokhandel.Models.Mockup.AUTHOR authors { get; set; }
        public MurvasBokhandel.Models.Mockup.BORROW borrows { get; set; }
    }
}