using Repository.EntityModel;
using Repository.Repository;

using Services.Service;
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
                if (PasswordService.VerifyPassword(password, UserRepository.dbGetPassword(email)))
                    return true;

            return false;
        }

        public static role GetRole(string email)
        {
            return UserRepository.dbGetUserRole(email);
        }
        public static void CreateUser(user u) {
            u.Password = PasswordService.CreateHash(u.Password);
            UserRepository.dbCreateUser(u);
        }
    }
}
