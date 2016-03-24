using Common.Model;
using Common.Share;
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

        public static void changePassword()
        {
            //u.Password = PasswordService.CreateHash(u.Password);

            //UserRepository.dbCreateUser(u);
        }
        
        public static void Update(BorrowerWithUser user, string password)
        {
            user.User.Password = PasswordService.CreateHash(password);

            UserRepository.dbUpdateUser(Auth.LoggedInUser.User.PersonId, user.User);
            BorrowerService.UpdateBorrower(user.Borrower);
        }
        
        public static bool emailExists(string inputEmail)
        {
            if (Repository.Repository.UserRepository.dbUserExists(inputEmail))
                return (true);
            else
                return (false);
        }
        
    }
}
