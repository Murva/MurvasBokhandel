using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityModel
{
    public class copy
    {
        public int Barcode { get; set; }
        public int StatusId { get; set; }
        public long ISBN { get; set; }
        public string Location { get; set; }
        public string Library { get; set; }
    }
}
