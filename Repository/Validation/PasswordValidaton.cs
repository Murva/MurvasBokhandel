using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Repository.Validation
{
    public class PasswordValidaton
    {
        public static string ErrorMessage = "Lösenord måste vara mellan 5-15 tecken långt och innehålla minst en siffra och en versal.";

        public static bool IsValid(string password)
        {
            if (password != null && Regex.IsMatch(password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{5,15}$"))
                return true;
            
            return false;            
        }    
    }
}
