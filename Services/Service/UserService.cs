using Common.Model;
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
    public class UserService
    {
        public static void changePassword()
        {
            //u.Password = PasswordService.CreateHash(u.Password);

            //UserRepository.dbCreateUser(u);
        }
        
        public static void update(BorrowerWithUser user)
        {
            //string newpassword = PasswordService.CreateHash(user.User.Password);
            user.User.Password = PasswordService.CreateHash(user.User.Password);
            UserRepository.dbUpdateUser(user.User);
            BorrowerService.UpdateBorrower(user.Borrower);

        }


    }
}
