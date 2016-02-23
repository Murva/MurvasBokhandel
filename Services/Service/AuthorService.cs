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
        public static AuthorWithBooks GetAuthorWithBooks(int aid)
        {
            return MapAuthor(AuthorRepository.dbGetAuthor(aid));
        }

        public static AuthorWithBooks MapAuthor(author a)
        {
            AuthorWithBooks authorwithbooks = new AuthorWithBooks();
            authorwithbooks.Author = a;
            authorwithbooks.Books = BookService.GetBooksByAuthor(a.Aid);

            return authorwithbooks;
        }
    }
}
