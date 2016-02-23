using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Repository.Repository;
using Repository.EntityModel;

namespace Services.Service
{
    public class AuthorService
    {
        public static Author getAuthor(int aid)
        {
            return MapAuthor(AuthorRepository.dbGetAuthor(aid));
        }

        public static Author MapAuthor(author a)
        {
            Author author = new Author();
            author.Aid = a.Aid;
            author.FirstName = a.FirstName;
            author.LastName = a.LastName;
            author.BirthYear = a.BirthYear;

            return author;
        }
    }
}
