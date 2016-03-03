using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.EntityModel;

namespace Common.Model
{
    public class AuthorsAndBooks
    {
        public List<author> Authors { get; set; }
        public List<book> Books { get; set; }
               
    }
}
