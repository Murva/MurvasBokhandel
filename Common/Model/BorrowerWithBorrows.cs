using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.EntityModel;
using Common.Model.Base;

namespace Common.Model
{
    public class BorrowerWithBorrows : BaseModel
    {
        public BorrowerWithUser BorrowerWithUser { get; set; }
        public ActiveAndHistoryBorrows Borrows { get; set; }
        public List<category> Categories { get; set; }
        public List<role> Roles { get; set; }
    }
}