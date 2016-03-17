using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Repository.Validation
{
    public class PersonIdValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //string pIdReg = "[1-2][0-9]{3}[0-1][1-9][0-3][1-9][-][0-9]{4}";
            string _personId = Convert.ToString(value);            
            if (!(Regex.IsMatch(_personId, "[1-2][0-9]{3}[0-1][1-9][0-3][1-9][-][0-9]{4}")))
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            else
            {
                return null;
            }
        }
    }
}
