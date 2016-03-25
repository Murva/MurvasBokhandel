using Repository.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class AuthorWithBooksAndBooks
    {
        public author Author { get; set; }
        public List<book> AuthorBooks { get; set; }
        public List<book> Books { get; set; }
    }
}
