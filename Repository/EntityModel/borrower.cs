using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Repository.EntityModel
{
    public class borrower
    {
        [Required(ErrorMessage = "Du måste fylla i personid")]
        public string PersonId { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Category mellan 1 och 4")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Förnamn")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Efernamn")]
        public string LastName { get; set; }

        public string Address { get; set; }
        [Phone(ErrorMessage="Du måste fylla i det på korrekt sätt")]
        public string Telno { get; set; }
    }
}
