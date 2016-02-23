using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Repository.Repository;
using Repository.EntityModel;

namespace Services.Service
{
    class BorrowService
    {
        public static BorrowedBookCopy GetBorrowedBooks(string PersonId) {
            return MapBorrow(BorrowRepository.dbGetBorrow(PersonId));    
        }

        public static BorrowedBookCopy MapBorrow(borrow b) {
            BorrowedBookCopy borrowedBookCopy = new BorrowedBookCopy();
            borrowedBookCopy.borrow = b;
            return borrowedBookCopy;
        }
    }
}
