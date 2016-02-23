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
        public status status { get; set; }
        public author author { get; set; }
        public borrow borrow { get; set; }
    }
}