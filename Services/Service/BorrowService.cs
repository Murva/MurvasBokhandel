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
    public class BorrowService
    {
        public static List<BorrowedBookCopy> GetBorrowedBooks(string PersonId) {
            return MapBorrow(BorrowRepository.dbGetBorrowList(PersonId));    
        }

        public static List<BorrowedBookCopy> MapBorrow(List<borrow> b) {
            List<BorrowedBookCopy> borrowedBookCopy = new List<BorrowedBookCopy>();
            foreach (borrow borrow in b)
            {
                BorrowedBookCopy bcopy = new BorrowedBookCopy();
                bcopy.borrow = borrow;
                bcopy.copy = CopyRepository.dbGetBookCopy(borrow.Barcode);
                bcopy.book = BookRepository.dbGetBook(bcopy.copy.ISBN);
                bcopy.authors = BookAuthorRepository.dbGetAuthorsByBook(bcopy.copy.ISBN);
                bcopy.status = StatusRepository.dbGetStatus(bcopy.copy.StatusId);
                bcopy.category = CategoryRepository.dbGetCategory(BorrowerRepository.dbGetBorrower( bcopy.borrow.PersonId).CategoryId);
                bcopy.fine = FineRepository.dbGetFine(borrow.Barcode, borrow.PersonId);
                borrowedBookCopy.Add(bcopy);
            }
            return borrowedBookCopy;
        }

        public static void updateBorrowDate(borrow b) {
            b.BorrowDate = DateTime.Today;
            BorrowRepository.updateDate(b);
        }
        public static void updateToBeReturnedDate(borrow b, int period) {
            b.ToBeReturnedDate = DateTime.Today.AddDays(period);
            BorrowRepository.updateDate(b);
        }

    }
}
