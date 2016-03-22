using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class BookRepository : BaseRepository<book>
    {
        public static book dbGetBook(string isbn) 
        {
            return dbGet("SELECT * FROM BOOK WHERE ISBN = @ISBN;", new SqlParameter[] {
                new SqlParameter("@ISBN", isbn)
            });
        }

        public static List<book> dbGetBookListByAuthor(int aid)
        {
            return dbGetList("SELECT * FROM BOOK as B, BOOK_AUTHOR as BA WHERE B.ISBN = BA.ISBN AND BA.Aid = @AID;", new SqlParameter[] {
                new SqlParameter("@AID", aid)
            });
        }

        public static List<book> dbGetBooks()
        {
            return dbGetList("SELECT * FROM BOOK ORDER BY TITLE", null);
        }

        private static SqlParameter[] _mapBookParameter(book b)
        {
            return new SqlParameter[] {
                new SqlParameter("@ISBN", b.ISBN),
                new SqlParameter("@TITLE", b.Title),
                new SqlParameter("@SIGNID", b.SignId),
                new SqlParameter("@PUBLICATIONYEAR", b.PublicationYear),
                new SqlParameter("@PUBLICATIONINFO", b.publicationinfo),
                new SqlParameter("@PAGES", b.pages)
            }; 
        }

        public static void dbUpdateBook(book b)
        {
            dbPost("UPDATE BOOK SET Title = @TITLE, PublicationYear = @PUBLICATIONYEAR, publicationinfo = @PUBLICATIONINFO, pages = @PAGES WHERE ISBN = @ISBN", _mapBookParameter(b));
        }

        public static void dbStoreBook(book b)
        {
            dbPost("INSERT INTO BOOK VALUES (@ISBN, @TITLE, @SIGNID, @PUBLICATIONYEAR, @PUBLICATIONINFO, @PAGES);", _mapBookParameter(b));
        }

        public static void dbRemoveBook(string ISBN)
        {
            dbPost("DELETE FROM BOOK WHERE ISBN = @ISBN;", new SqlParameter[] { 
                new SqlParameter("@ISBN", ISBN)
            });
        }
        
        public static List<book> dbGetBooksBySearch(string search)
        {
            return dbGetList("SELECT * FROM Book WHERE Title LIKE '%'+@SEARCH+'%';", new SqlParameter[] { 
                new SqlParameter("@SEARCH", search)
            });
        }
        
        public static List<book> dbGetBooksByLetter(string letter)
        {
            return dbGetList("SELECT * FROM Book WHERE Title LIKE @LETTER+'%';", new SqlParameter[] { 
                new SqlParameter("@LETTER", letter)
            });
        }
        
        public static List<book> dbGetBooksByNumber(List<string> number)
        {
            return dbGetList("SELECT * FROM Book WHERE Title LIKE @NUMBER0+'%' OR Title LIKE @NUMBER1+'%' OR Title LIKE @NUMBER2+'%' OR Title LIKE @NUMBER3+'%' OR Title LIKE @NUMBER4+'%' OR Title LIKE @NUMBER5+'%' OR Title LIKE @NUMBER6+'%' OR Title LIKE @NUMBER7+'%' OR Title LIKE @NUMBER8+'%' OR Title LIKE @NUMBER9+'%';", new SqlParameter[] { 
                new SqlParameter("@NUMBER0", number[0]),
                new SqlParameter("@NUMBER1", number[1]),
                new SqlParameter("@NUMBER2", number[2]),
                new SqlParameter("@NUMBER3", number[3]),
                new SqlParameter("@NUMBER4", number[4]),
                new SqlParameter("@NUMBER5", number[5]),
                new SqlParameter("@NUMBER6", number[6]),
                new SqlParameter("@NUMBER7", number[7]),
                new SqlParameter("@NUMBER8", number[8]),
                new SqlParameter("@NUMBER9", number[9]),

            });
        }
    }
}
