using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Repository.Validation
{
    public class IsbnValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string _isbn = Convert.ToString(value);
            if (!(_isbn.Length <= 10 && Regex.IsMatch(_isbn, "^[0-9X]+$")))
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));

            return null;            
        }
    }
}
