using Repository.Repository;
using Service.Service;
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
    }
}
