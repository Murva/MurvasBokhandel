using Repository.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class BorrowerWithUser : Base.BaseModel
    {
        public borrower Borrower { get; set; }
        public user User { get; set; }
    }
}
