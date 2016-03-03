using Repository.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class BookWithAuthorsAndAuthors
    {
        public book Book { get; set; }
        public List<author> BookAuthors { get; set; }
        public List<author> Authors { get; set; }
    }
}
