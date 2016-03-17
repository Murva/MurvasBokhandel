using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Repository.Validation
{
    public class AdressValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string _adress = Convert.ToString(value);
            if (Regex.IsMatch(_adress, "[\\wåäöÅÄÖ\\.-]*"))
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            return null;            
        }    
    }
}
