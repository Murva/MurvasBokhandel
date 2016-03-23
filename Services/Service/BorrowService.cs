using System;
using System.Collections.Generic;
using Common.Model;
using Repository.Repository;
using Repository.EntityModel;

namespace Services.Service
{
    public class BorrowService
    {
        public static List<BorrowedBookCopy> GetBorrowedBooks(string PersonId) {
            return MapBorrow(BorrowRepository.dbGetBorrowListByPersonId(PersonId));    
        }

        public static List<BorrowedBookCopy> MapBorrow(List<borrow> b) {
            List<BorrowedBookCopy> borrowedBookCopy = new List<BorrowedBookCopy>();
            foreach (borrow borrow in b)
            {
                BorrowedBookCopy bcopy = new BorrowedBookCopy();
                bcopy.borrow = borrow;
                bcopy.copy = CopyRepository.dbGetCopyByBarcode(borrow.Barcode);
                bcopy.book = BookRepository.dbGetBook(bcopy.copy.ISBN);
                bcopy.authors = AuthorRepository.dbGetAuthorsByBookISBN(bcopy.copy.ISBN);
                bcopy.status = StatusRepository.dbGetStatusByStatusId(bcopy.copy.StatusId);
                bcopy.category = CategoryRepository.dbGetCategoryById(BorrowerRepository.dbGetBorrower( bcopy.borrow.PersonId).CategoryId);
                bcopy.fine = FineRepository.dbGetFine(borrow.Barcode, borrow.PersonId);
                borrowedBookCopy.Add(bcopy);
            }
            return borrowedBookCopy;
        }

        //public static void updateBorrowDate(borrow b) {
        //    b.BorrowDate = DateTime.Today;
        //    BorrowRepository.updateDate(b);
        //}

        //public static void updateToBeReturnedDate(borrow b, int period) {
        //    b.ToBeReturnedDate = DateTime.Today.AddDays(period);
        //    BorrowRepository.updateDate(b);
        //}

        public static void RenewLoad(borrower br, string barcode)
        {
            DateTime ToBeReturnedDate = DateTime.Today.AddDays(CategoryRepository.dbGetCategoryById(br.CategoryId).Period);
            BorrowRepository.dbUpdateBorrowDates(br.PersonId, barcode, ToBeReturnedDate);
        }

        public static void RenewAllLoans(borrower br, List<BorrowedBookCopy> borrowes)
        {
            DateTime ToBeReturnedDate = DateTime.Today.AddDays(CategoryRepository.dbGetCategoryById(br.CategoryId).Period);
            foreach (BorrowedBookCopy b in borrowes)
                BorrowRepository.dbUpdateBorrowDates(br.PersonId, b.borrow.Barcode, ToBeReturnedDate);
        }
    }
}
