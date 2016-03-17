using Repository.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityModel
{
    public class book
    {
        [IsbnValidation(ErrorMessage = "ISBN måste vara mindre än 10 tecken och sluta på X eller en siffra.")]
        public string ISBN { get; set; }
        [Required]
        public int SignId { get; set; }
        [Required]
        public string PublicationYear { get; set; }
        [Required]
        public string Title { get; set; }
        public string publicationinfo { get; set; }
        [Required]
        public int pages { get; set; }
    }
}
