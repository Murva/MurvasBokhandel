using Repository.EntityModel;
using Repository.Repository;
using System.Collections.Generic;

namespace Services.Service
{
    public class BookService
    {
        public static List<book> GetBooksByAuthor(int aid)
        {
            return BookRepository.dbGetBookListByAuthor(aid);
        }
        public static List<book> GetBooks()
        {
            return BookRepository.dbGetBooks();
        }

    }
}
