using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.EntityModel;

namespace Common.Model
{
    public class LettersAndBooks : Base.LettersAndModel<book>
    {
        public LettersAndBooks(List<string> letters, List<book> books) : base(letters, books) 
        {}
    }
}
