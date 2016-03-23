using Common.Model;
using Repository.EntityModel;
using Repository.Repository;

namespace Services.Service
{
    public class UserService
    {
        public static void changePassword()
        {
            //u.Password = PasswordService.CreateHash(u.Password);

            //UserRepository.dbCreateUser(u);
        }
        
        public static void update(BorrowerWithUser user, string newpassword)
        {
            //string newpassword = PasswordService.CreateHash(user.User.Password);
            user.User.Password = PasswordService.CreateHash(newpassword);
            UserRepository.dbUpdateUser(user.User);
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
