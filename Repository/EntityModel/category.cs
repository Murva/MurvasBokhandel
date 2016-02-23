using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityModel
{
    public class category
    {
        public int CategoryId {get; set;}
        public int Penaltyperday { get; set; }
        public string Category { get; set; } 
        public byte Period {get; set;}
    }
}
