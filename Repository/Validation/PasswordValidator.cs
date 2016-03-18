using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Repository.Validation
{
    public class PasswordValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string _password = Convert.ToString(value);
            if (!(Regex.IsMatch(_password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{5,15}$")))
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            return null;            
        }    
    }
}
