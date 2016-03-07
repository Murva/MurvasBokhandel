using Repository.EntityModel;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class AuthService
    {
        public static bool Login(string email, string password)
        {
            if (UserRepository.dbUserExists(email))
                if (UserRepository.dbCheckPassword(email, PasswordService.CreateHash(password)))
                    return true;

            return false;
        }

        public static role GetRole(string email)
        {
            return UserRepository.dbGetUserRole(email);
        }
    }
}
