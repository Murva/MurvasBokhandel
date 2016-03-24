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
        public borrower borrower { get; set; }
        public List<category> categories { get; set; }
        public int CatergoryId { get; set; }
    }
}