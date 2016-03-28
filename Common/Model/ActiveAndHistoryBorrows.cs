using System.Collections.Generic;

namespace Common.Model
{
    public class ActiveAndHistoryBorrows
    {
        public List<BorrowedBookCopy> Active { get; set; }
        public List<BorrowedBookCopy> History { get; set; }
    }
}
