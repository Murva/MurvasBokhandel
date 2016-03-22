using System;
using Repository.EntityModel;
using Repository.Repository;
using Common.Model;
using System.Collections.Generic;
namespace Services.Service
{
    public class BorrowerService
    {
        public static bool checkIfBorrowerExists(string PersonId) {
            borrower b = BorrowerRepository.dbGetBorrower(PersonId);
            if (b.PersonId == null)
                return false;
            else return true;
        }
        public static List<borrower> getBorrowers() {
            return BorrowerRepository.dbGetBorrowers();
        }

        public static BorrowerWithBorrows GetBorrower(string PersonId)
        {
            return mapBorrowerWithBorrows(BorrowerRepository.dbGetBorrower(PersonId));
        }
        public static BorrowerWithUser GetBorrowerWithUser(string Email)
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
            borrowerwithborrows.Borrows = BorrowRepository.dbGetBorrowListByPersonId(b.PersonId);
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

        public static BorrowerWithUser GetBorrowerWithUser()
        {
            throw new NotImplementedException();
        }
    }
}
