using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.EntityModel;

namespace Common.Model
{
    public class BorrowerWithBorrows
    {
        public borrower Borrower { get; set; }
        public List<borrow> Borrows { get; set; }
        public List<category> Categories { get; set; }
    }
}