using Repository.EntityModel;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class BookRepository : Base.BaseRepository
    {
        private static book mapBook(SqlDataReader dar)
        {
            book _book = new book();
            _book.ISBN = dar["ISBN"] as string;
            _book.pages = Convert.ToInt32(dar["Pages"]);
            _book.Title = dar["Title"] as string;
            _book.publicationinfo = dar["publicationinfo"] as string;
            _book.PublicationYear = dar["PublicationYear"] as string;
            _book.SignId = Convert.ToInt32(dar["SignId"]);

            return _book;
        }


        public static book dbGetBook(string isbn) {

            book _book = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            SqlCommand cmd = new SqlCommand("SELECT * FROM BOOK WHERE ISBN = @ISBN;", con);
            
            cmd.Parameters.AddWithValue("@ISBN", isbn);
            
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar.Read())
                {
                    _book = mapBook(dar);
                }
            }
            catch (Exception eObj)
            {
                throw eObj;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return _book;
        }
        private static List<book> dbGetBookList(string query, SqlParameter[] sp)
        {
            List<book> _bookList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _bookList = new List<book>();
                    while (dar.Read())
                    {
                        _bookList.Add(mapBook(dar));
                    }
                }
            }
            catch (Exception eObj)
            {
                throw eObj;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return _bookList;
        }

        public static List<book> dbGetBookListByAuthor(int aid)
        {
            return dbGetBookList("SELECT * FROM BOOK as B, BOOK_AUTHOR as BA WHERE B.ISBN = BA.ISBN AND BA.Aid = @AID;", new SqlParameter[] {
                new SqlParameter("@AID", aid)
            });
        }
        public static List<book> dbGetBooks()
        {
            return dbGetBookList("SELECT * FROM BOOK ORDER BY TITLE", null);
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
            dbPostData("UPDATE BOOK SET Title = @TITLE, PublicationYear = @PUBLICATIONYEAR, publicationinfo = @PUBLICATIONINFO, pages = @PAGES WHERE ISBN = @ISBN", _mapBookParameter(b));
        }

        public static void dbStoreBook(book b)
        {
            dbPostData("INSERT INTO BOOK VALUES (@ISBN, @TITLE, @SIGNID, @PUBLICATIONYEAR, @PUBLICATIONINFO, @PAGES);", _mapBookParameter(b));
        }

        public static void dbRemoveBook(string ISBN)
        {
            dbPostData("DELETE FROM BOOK WHERE ISBN = @ISBN;", new SqlParameter[] { 
                new SqlParameter("@ISBN", ISBN)
            });
        }
        public static List<book> dbGetBooksBySearch(string search)
        {
            return dbGetBookList("SELECT * FROM Book WHERE Title LIKE '%'+@SEARCH+'%';", new SqlParameter[] { 
                new SqlParameter("@SEARCH", search)
            });
        }
    }
}
