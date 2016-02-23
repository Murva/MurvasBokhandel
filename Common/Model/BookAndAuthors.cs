using System.Collections.Generic;
using Repository.EntityModel;

namespace Common.Model
{
    public class BookAndAuthors
    {
        public book Book { get; set; }
        public List<author> Authors { get; set; }
        public int Aid { get; set; }
    }
}