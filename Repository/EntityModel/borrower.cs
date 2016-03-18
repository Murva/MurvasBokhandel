using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Repository.Validation;


namespace Repository.EntityModel
{
    public class borrower
    {
        [Required(ErrorMessage = "Borrower Du måste fylla i personid")]
        [PersonIdValidation(ErrorMessage="Borrowe Du måste fylla i personnummret på \"ÅÅÅÅMMDD-XXXX\"")]
        public string PersonId { get; set; }

        [Required(ErrorMessage = "Borrower Du måste fylla i Category mellan 1 och 4")]
        public int CategoryId { get; set; }
    

        [Required(ErrorMessage = "borrower Du måste fylla i Förnamn")]
        [NameValidation(ErrorMessage="Borrower Namn för bra innehålla bokstäver")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "borrowe Du måste fylla i Efernamn")]
        [NameValidation(ErrorMessage = "booobb Namn för bra innehålla bokstäver")]
        public string LastName { get; set; }

        [AdressValidation(ErrorMessage=" booobbo Vanliga tecken samt . - ")]
        public string Address { get; set; }

        [Phone(ErrorMessage="booo bo Du måste fylla i det på korrekt sätt")]
        public string Telno { get; set; }
    }
}
