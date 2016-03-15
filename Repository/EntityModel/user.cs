using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Repository.EntityModel
{
    
    public class user
    {
        [Required(ErrorMessage = "Du måste fylla i personid")]
        public string PersonId { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Förnamn")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Efternamn")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Epost")]
        public string Email { get; set; }
        public string Address { get; set; }

        [Phone(ErrorMessage ="Ditt telefonnummer måste fyllas i på rätt sätt")]
        public string Telno { get; set; }
        [Required(ErrorMessage = "Du måste välja RoleId")]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Lösenord")]
        public string Password { get; set; }
    }
}
