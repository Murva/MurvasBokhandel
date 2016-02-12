using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MurvasBokhandel.Models
{
    public class BorrowerWithBorrows
    {
        public Mockup.BORROWER Borrower { get; set; }
        public List<Mockup.BORROW> Borrows { get; set; }
    }
}