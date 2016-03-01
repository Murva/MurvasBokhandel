using System.Collections.Generic;

namespace Common.Model
{
    public class BookWithClassifications
    {
        public Repository.EntityModel.book Book {get; set;}
        public List<Repository.EntityModel.classification> Classifications { get; set; }
    }
}
