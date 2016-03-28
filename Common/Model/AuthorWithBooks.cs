using System.Collections.Generic;
using Repository.EntityModel;

namespace Common.Model
{
    public class AuthorWithBooks
    {
        public author Author { get; set; }
        public List<book> Books { get; set; }
    }
}