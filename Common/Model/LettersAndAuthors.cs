﻿using Repository.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class LettersAndAuthors : Base.LettersAndModel<author>
    {
        public LettersAndAuthors(List<string> letters, List<author> authors) : base(letters, authors)
        {}
    }
}
