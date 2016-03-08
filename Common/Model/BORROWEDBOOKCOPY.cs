using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.EntityModel;

namespace Common.Model
{
    public class BorrowedBookCopy
    {
        public book book { get; set; }
        public copy copy { get; set; }
        public Status status { get; set; }
        public List<author> authors { get; set; }
        public borrow borrow { get; set; }
        public int fine { get; set; }
    }
}