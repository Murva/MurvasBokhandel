using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityModel
{
    public class user
    {
        [Required(ErrorMessage = "Du måste fylla i personid")]
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Du måste fylla i en email")]
        [EmailAddress(ErrorMessage = "Fyll i en giltig email")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Telno { get; set; }
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Fyll i ett lösenord")]
        public string Password { get; set; }
    }
}
