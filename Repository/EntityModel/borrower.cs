using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityModel
{
    public class borrower
    {
        [Required(ErrorMessage = "Du måste fylla i Personnummer")]
        public string PersonId { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Kategori")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Förnamn")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Efternamn")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Tel.nr")]
        public string Telno { get; set; }
    }
}
