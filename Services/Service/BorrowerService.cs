using System;
using Repository.EntityModel;
using Repository.Repository;
using Common.Model;
using System.Collections.Generic;
namespace Services.Service
{
    public class BorrowerService
    {
        public static List<borrower> getBorrowers() {
            return BorrowerRepository.dbGetBorrowerList("SELECT * FROM borrower");
        }

        public static BorrowerWithBorrows GetBorrower(string PersonId)
        {
            return mapBorrowerWithBorrows(BorrowerRepository.dbGetBorrower(PersonId));
        }

        private static BorrowerWithBorrows mapBorrowerWithBorrows(borrower b)
        {
            BorrowerWithBorrows borrowerwithborrows = new BorrowerWithBorrows();
            borrowerwithborrows.BorrowerWithUser = new BorrowerWithUser();
            borrowerwithborrows.BorrowerWithUser.Borrower = b;
            borrowerwithborrows.Borrows = BorrowRepository.dbGetBorrowList(b.PersonId);
            borrowerwithborrows.Categories = CategoryService.getCategories();
            borrowerwithborrows.BorrowerWithUser.User = UserRepository.dbGetUser("SELECT * FROM \"USER\" WHERE PersonId = '" + b.PersonId + "';");
            
            return borrowerwithborrows;
        }
        public static void RemoveBorrower(borrower b) { 
            BorrowerRepository.dbRemoveBorrower(b);
            BorrowRepository.dbRemoveBorrowsByPersonId(b.PersonId);
        }
        public static void UpdateBorrower(borrower b)
        {
            BorrowerRepository.dbUpdateBorrower(b);
        }
        public static void StoreBorrower(borrower b){
            BorrowerRepository.dbStoreBorrower(b);
        }

        public static BorrowerWithUser GetBorrowerWithUser()
        {
            throw new NotImplementedException();
        }
    }
}
