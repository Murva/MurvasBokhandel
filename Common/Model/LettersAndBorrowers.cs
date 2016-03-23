using Repository.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class LettersAndBorrowers : Base.LettersAndModel<borrower>
    {
        public LettersAndBorrowers(List<string> letters, List<borrower> borrowers) : base(letters, borrowers)
        {}
    }
}
