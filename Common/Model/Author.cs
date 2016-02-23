using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Author
    {
        public int Aid { get; set; }
        public int BirthYear { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Repository.EntityModel.book> Books;
    }
}
