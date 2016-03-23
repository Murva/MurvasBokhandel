using System;
using Repository.EntityModel;
using Repository.Repository;
using Common.Model;
using System.Collections.Generic;
namespace Services.Service
{
    public class BorrowerService
    {
        public static bool CheckIfBorrowerExists(string PersonId) {
            return (BorrowerRepository.dbGetBorrower(PersonId) == null ? false : true);
        }

        public static List<borrower> GetBorrowers() {
            return BorrowerRepository.dbGetBorrowers();
        }

        public static borrower GetBorrower(string PersonId)
        {
            return BorrowerRepository.dbGetBorrower(PersonId);
        }

        public static BorrowerWithBorrows GetBorrowerWithBorrows(string PersonId)
        {
            return mapBorrowerWithBorrows(BorrowerRepository.dbGetBorrower(PersonId));
        }

        public static BorrowerWithUser GetBorrowerWithUserByEmail(string Email)
        {
            BorrowerWithUser activeUser = new BorrowerWithUser();
            activeUser.User = AuthService.GetUser(Email);
            activeUser.Borrower = BorrowerRepository.dbGetBorrower(activeUser.User.PersonId);
           
            return activeUser;
        }
        
        public static BorrowerWithUser GetBorrowerWithUserByPersonId(string PersonId)
        {
            BorrowerWithUser activeUser = new BorrowerWithUser();
            activeUser.User = AuthService.GetUserByPersonId(PersonId);
            activeUser.Borrower = BorrowerRepository.dbGetBorrower(activeUser.User.PersonId);

            return activeUser;
        }

        private static BorrowerWithBorrows mapBorrowerWithBorrows(borrower b)
        {
            BorrowerWithBorrows borrowerwithborrows = new BorrowerWithBorrows();
            borrowerwithborrows.BorrowerWithUser = new BorrowerWithUser();
            
            borrowerwithborrows.BorrowerWithUser.Borrower = b;
            borrowerwithborrows.Borrows = new ActiveAndHistoryBorrows();
            borrowerwithborrows.Borrows.active = BorrowService.GetActiveBorrowedBooks(b.PersonId);
            borrowerwithborrows.Borrows.history = BorrowService.GetHistoryBorrowedBooks(b.PersonId);
            borrowerwithborrows.Categories = CategoryService.getCategories();
            borrowerwithborrows.BorrowerWithUser.User = UserRepository.dbGetUserByPersonId(b.PersonId);
            if (borrowerwithborrows.BorrowerWithUser.User == null)
                borrowerwithborrows.BorrowerWithUser.User = new user();
            borrowerwithborrows.Roles = RoleRepository.dbGetRoles();
            return borrowerwithborrows;
        }
        
        public static void RemoveBorrower(borrower b) {
            string PersonId = b.PersonId;
            BorrowRepository.dbRemoveBorrowsByPersonId(PersonId);
            UserRepository.dbRemoveUser(PersonId);
            BorrowerRepository.dbRemoveBorrower(b);
        }
        public static void UpdateBorrower(borrower b)
        {
            BorrowerRepository.dbUpdateBorrower(b);
        }
        public static void StoreBorrower(borrower b){
            BorrowerRepository.dbStoreBorrower(b);
        }

        public static List<borrower> GetBorrowersByLetter(string letter)
        {
            return BorrowerRepository.dbGetBorrowersByLetter(letter);
        }
    }
}
