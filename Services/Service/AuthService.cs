using Repository.EntityModel;
using Repository.Repository;


namespace Services.Service
{
    public class AuthService
    {
        public static bool Login(string email, string password)
        {
            if (UserRepository.dbUserExists(email))
                if (PasswordService.VerifyPassword(password, UserRepository.dbGetPassword(email)))
                    return true;

            return false;
        }
        
        public static user GetUser(string email)
        {
            return UserRepository.dbGetUserByEmail(email);
        }

        public static user GetUserByPersonId(string PersonId)
        {
            return UserRepository.dbGetUserByPersonId(PersonId);
        }

        public static role GetRole(string email)
        {
            return RoleRepository.dbGetRoleByUserEmail(email);
        }

        public static void CreateUser(user u) {
            u.Password = PasswordService.CreateHash(u.Password);
            UserRepository.dbCreateUser(u);
        }
    }
}
