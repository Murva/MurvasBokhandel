using System;
using Repository.EntityModel;
using Repository.Repository;
using Common.Model;
namespace Services.Service
{
    public class BorrowerService
    {
        public static BorrowerWithBorrows GetBorrower(string PersonId)
        {
            return mapBorrowerWithBorrows(BorrowerRepository.GetBorrower(PersonId));
        }

        private static BorrowerWithBorrows mapBorrowerWithBorrows(borrower b)
        {
            BorrowerWithBorrows borrowerwithborrows = new BorrowerWithBorrows();
            borrowerwithborrows.Borrower = b;
            //borrowerwithborrows.Borrows = BorrowRepository.db

            return borrowerwithborrows;
        }
    }
}
