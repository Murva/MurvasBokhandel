using Common.Model;
using Repository.EntityModel;
using Repository.Repository;

using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
        public static bool IsEmail(string inputEmail)
        {

            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail) && (Repository.Repository.UserRepository.dbUserExists(inputEmail)))
                return (true);
            else
                return (false);
        }


    }
}
