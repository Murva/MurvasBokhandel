using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Repository.Validation
{
    public class NameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string _namn = Convert.ToString(value);
            if (!(Regex.IsMatch(_namn, "^[a-zA-ZåäöÅÄÖ-]{2,20}$")))
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            return null;  
        }
    }
}
