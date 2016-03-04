using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.EntityModel;

namespace Common.Model
{
    public class BorrowerAndCategories
    {
        public borrower borrower { get; set; }
        public List<category> categories { get; set; }
        public int CatergoryId { get; set; }
    }
}