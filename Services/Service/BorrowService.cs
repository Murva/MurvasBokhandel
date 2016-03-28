using System;
using System.Collections.Generic;
using Common.Model;
using Repository.Repository;
using Repository.EntityModel;

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
            List<BorrowedBookCopy> borrowedBookCopy = new List<BorrowedBookCopy>();
            foreach (borrow borrow in b)
            {
                copy c = CopyRepository.dbGetCopyByBarcode(borrow.Barcode);
                borrowedBookCopy.Add(new BorrowedBookCopy() { 
                    Borrow = borrow,
                    Authors = AuthorRepository.GetAuthorsByBookISBN(c.ISBN),
                    Book = BookRepository.dbGetBook(c.ISBN),
                    Category = CategoryRepository.dbGetCategoryById(BorrowerRepository.dbGetBorrower(borrow.PersonId).CategoryId),
                    Fine = FineRepository.dbGetFine(borrow.Barcode, borrow.PersonId)
                });
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
                BorrowRepository.dbUpdateBorrowDates(br.PersonId, b.Borrow.Barcode, ToBeReturnedDate);
        }
    }
}
