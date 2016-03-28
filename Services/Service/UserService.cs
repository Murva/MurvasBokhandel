using Common.Model;
using Common;
using Repository.EntityModel;
using Repository.Repository;

namespace Services.Service
{
    public class UserService
    {
        public static ActiveAndHistoryBorrows GetActiveAndHistoryBorrows(string PersonId) 
        {
            return new ActiveAndHistoryBorrows()
            {
                Active = BorrowService.GetActiveBorrowedBooks(PersonId),
                History = BorrowService.GetHistoryBorrowedBooks(PersonId)
            };
        }

        public static void ChangePassword()
        {
            //u.Password = PasswordService.CreateHash(u.Password);

            //UserRepository.dbCreateUser(u);
        }
        
        public static void Update(BorrowerWithUser user, string password)
        {
            if (password != null)
                user.User.Password = PasswordService.CreateHash(password);
            else
                user.User.Password = AuthService.GetUserByPersonId(user.User.PersonId).Password;

            UserRepository.dbUpdateUser(user.User.PersonId, user.User);
            BorrowerService.UpdateBorrower(user.Borrower);
        }
        
        public static bool EmailExists(string inputEmail)
        {
            if (Repository.Repository.UserRepository.dbUserExists(inputEmail))
                return (true);
            else
                return (false);
        }

        public static bool HasUserPermissions(int roleId)
        {
            if (roleId >= 1)
                return true;

            return false;
        }

        public static bool HasAdminPermissions(int roleId)
        {
            if (roleId == 2)
                return true;

            return false;
        }

        public static bool BorrowerIsUser(BorrowerWithUser b, string PersonId)
        {
            if (b.User.PersonId == PersonId)
                return true;

            return false;
        }
        
    }
}
