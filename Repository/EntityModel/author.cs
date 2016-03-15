using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityModel
{
    public class author
    {
        [Required(ErrorMessage = "Du måste fylla i personid")]
        public int Aid { get; set; }
        public string BirthYear { get; set; }

        [Required(ErrorMessage = "Du måste fylla i förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Du måste fylla i efternamn")]
        public string LastName { get; set; }
    }
}
