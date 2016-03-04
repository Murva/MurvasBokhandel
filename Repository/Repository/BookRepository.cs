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
            SqlCommand cmd = new SqlCommand("SELECT * FROM BOOK WHERE ISBN = '" + isbn + "';", con);
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
        private static List<book> dbGetBookList(string query)
        {
            List<book> _bookList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
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

        public static List<book> dbGetBookListByAuthor(int aid, string orderby = "Title")
        {
            return dbGetBookList("SELECT * FROM BOOK as B, BOOK_AUTHOR as BA WHERE B.ISBN = BA.ISBN AND BA.Aid = " + aid.ToString() + " ORDER BY B." + orderby + ";");
        }
        public static List<book> dbGetBooks()
        {
            return dbGetBookList("SELECT * FROM BOOK");
        }


        private static void dbPostData(string query)
        {
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        public static void dbUpdateBook(book b)
        {
            dbPostData("UPDATE BOOK SET Title = '" + b.Title + "', PublicationYear = '" + b.PublicationYear.ToString() + "', publicationinfo = '" + b.publicationinfo.ToString() + "', pages = " + b.pages.ToString() + " WHERE ISBN = '" + b.ISBN.ToString() + "'");
        }

        public static void dbStoreBook(book b)
        {
            dbPostData("INSERT INTO BOOK VALUES ('"+b.ISBN+"','"+b.Title+"' , "+b.SignId.ToString()+", '"+b.PublicationYear+"', '"+b.publicationinfo+"', "+b.pages.ToString()+");");
        }

        public static void dbRemoveBook(string ISBN)
        {
            dbPostData("DELETE FROM BOOK WHERE ISBN = '"+ISBN+"';");
        }
        public static List<book> dbGetBooksBySearch(string search)
        {
            return dbGetBookList("SELECT * FROM Book WHERE Title LIKE '%" + search + "%';");
        }
    }
}
