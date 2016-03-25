using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.EntityModel;
using Common.Model.Base;

namespace Common.Model
{
    public class BorrowerAndCategories : BaseModel
    {
        public borrower Borrower { get; set; }
        public List<category> Categories { get; set; }
        public int CatergoryId { get; set; }
    }
}