using Common.Model;
using Common;
using Repository.EntityModel;
using Repository.Repository;

namespace Services.Service
{
    public class UserService
    {
        public static ActiveAndHistoryBorrows GetActiveAndHistoryBorrows() 
        {
            return new ActiveAndHistoryBorrows()
            {
                Active = BorrowService.GetActiveBorrowedBooks(Auth.LoggedInUser.User.PersonId),
                History = BorrowService.GetHistoryBorrowedBooks(Auth.LoggedInUser.User.PersonId)
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

            //UserRepository.dbUpdateUser(Auth.LoggedInUser.User.PersonId, user.User);
            UserRepository.dbUpdateUser(user.User);
            BorrowerService.UpdateBorrower(user.Borrower);
        }
        
        public static bool EmailExists(string inputEmail)
        {
            if (Repository.Repository.UserRepository.dbUserExists(inputEmail))
                return (true);
            else
                return (false);
        }
        
    }
}
