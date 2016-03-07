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
        public static List<author> GetAuthors(string orderBy)
        {
            return AuthorRepository.dbGetAuthors(orderBy);
        }

        public static AuthorWithBooks GetAuthorWithBooks(int aid)
        {
            return MapAuthorWithBooks(AuthorRepository.dbGetAuthor(aid));
        }

        public static AuthorWithBooksAndBooks GetAuthorWithBooksAndBooks(int aid)
        {
            return MapAuthorWithBooksAndClassifications(GetAuthorWithBooks(aid));
        }

        public static AuthorWithBooksAndBooks MapAuthorWithBooksAndClassifications(AuthorWithBooks a)
        {
            AuthorWithBooksAndBooks aw = new AuthorWithBooksAndBooks();
            aw.Author = a.Author;
            aw.AuthorBooks = a.Books;
            aw.Books = BookService.GetBooks("Title");

            return aw;
        }

        public static AuthorWithBooks MapAuthorWithBooks(author a)
        {
            AuthorWithBooks authorwithbooks = new AuthorWithBooks();
            authorwithbooks.Author = a;
            authorwithbooks.Books = BookService.GetBooksByAuthor(a.Aid);

            return authorwithbooks;
        }
        public static List<author> GetAuthersByBook(string isbn)
        {
            return BookAuthorRepository.dbGetAuthorsByBook(isbn);
        }

        public static bool AuthorExists(int Aid)
        {
            return (AuthorRepository.dbGetAuthor(Aid) != null ? true : false);
        }

        public static void UpdateAuthor(author a)
        {
            AuthorRepository.UpdateAuthor(a);
        }

        public static void StoreAuthor(author a)
        {
            AuthorRepository.StoreAuthor(a);
        }

        public static void DeleteAuthor(author a)
        {
            AuthorRepository.DeleteAuthor(a);
        }

        public static AuthorsAndBooks GetSearchResult(string input)
        {
            AuthorsAndBooks authorsandbooks = new AuthorsAndBooks();
            authorsandbooks.Authors = AuthorRepository.dbGetAuthorsBySearch(input);
            authorsandbooks.Books = BookService.GetBooksBySearch(input);

            return authorsandbooks;
        }
    }
}
