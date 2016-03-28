using Common.Model;
using Repository.EntityModel;
using Repository.Repository;
using System;
using System.Collections.Generic;

namespace Services.Service
{
    public class BorrowService
    {
        public static List<BorrowedBookCopy> GetActiveBorrowedBooks(string PersonId) {
            return MapBorrow(BorrowRepository.dbGetActiveBorrowListByPersonId(PersonId));    
        }
        public static List<BorrowedBookCopy> GetHistoryBorrowedBooks(string PersonId) {
            return MapBorrow(BorrowRepository.dbGetHistoryBorrowListByPersonId(PersonId));
        }
        public static List<BorrowedBookCopy> MapBorrow(List<borrow> b) {
            List<BorrowedBookCopy> BorrowedBookCopy = new List<BorrowedBookCopy>();
            foreach (borrow borrow in b)
            {
                copy c = CopyRepository.dbGetCopyByBarcode(borrow.Barcode);
                BorrowedBookCopy.Add(new BorrowedBookCopy() { 
                    borrow = borrow,
                    authors = AuthorRepository.dbGetAuthorsByBookISBN(c.ISBN),
                    book = BookRepository.dbGetBook(c.ISBN),
                    category = CategoryRepository.dbGetCategoryById(BorrowerRepository.dbGetBorrower(borrow.PersonId).CategoryId),
                    fine = FineRepository.dbGetFine(borrow.Barcode, borrow.PersonId)
                });
            }
            return BorrowedBookCopy;
        }

        public static void RenewLoan(borrower br, string barcode)
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
