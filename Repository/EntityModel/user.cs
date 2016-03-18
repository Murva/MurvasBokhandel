using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Repository.Validation;

namespace Repository.EntityModel
{
    
    public class user
    {
        [Required(ErrorMessage = "Du måste fylla i personid")]
        [PersonIdValidation(ErrorMessage="Du måste fylla i personnummret på \"ÅÅÅÅMMDD-XXXX\"")]
        public string PersonId { get; set; }

        //[Required(ErrorMessage = "Du måste fylla i Förnamn")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Du måste fylla i Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Du måste fylla i Epost")]
        [EmailValidation(ErrorMessage ="Du måste fylla i en korrekt Epost")]
        public string Email { get; set; }
        
        public string Address { get; set; }

        //[Phone(ErrorMessage ="Ditt telefonnummer måste fyllas i på rätt sätt")]
        public string Telno { get; set; }
        [Required(ErrorMessage = "Du måste välja RoleId")]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Du måste fylla i Lösenord")]
        [PasswordValidator(ErrorMessage="Läsenordet måste vara mellan 5 och 15 tecken samt innehålla en Versal och Siffra")]        
        public string Password { get; set; }
    }
}
