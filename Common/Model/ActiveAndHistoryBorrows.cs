﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class ActiveAndHistoryBorrows
    {
        public List<BorrowedBookCopy> Active { get; set; }
        public List<BorrowedBookCopy> History { get; set; }
    }
}
