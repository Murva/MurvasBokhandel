using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityModel
{
    public class book
    {
        public long ISBN { get; set; }
        public int SignId { get; set; }
        public int PublicationYear { get; set; }
        public string Title { get; set; }
        public string Publicationinfo { get; set; }
        public string Pages { get; set; }
    }
}
