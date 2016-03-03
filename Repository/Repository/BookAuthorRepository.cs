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
    public class BookAuthorRepository : Base.BaseRepository
    {
        static public List<author> dbGetAuthorsByBook(string isbn){
            List<author> _authorList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM BOOK_AUTHOR INNER JOIN AUTHOR ON BOOK_AUTHOR.Aid=AUTHOR.Aid WHERE BOOK_AUTHOR.ISBN = '"+isbn+"';", con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _authorList = new List<author>();
                    while (dar.Read())
                    {
                        _authorList.Add(AuthorRepository.MapAuthor(dar));
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

            return _authorList;
        }

        public static bookAuthor MapAuthor(SqlDataReader dar)
        {
            return new bookAuthor()
            {
                Aid = Convert.ToInt32(dar["Aid"]),
                ISBN = dar["ISBN"] as string
            };
        }

        public static bookAuthor dbGetBookAuthor(int Aid, string ISBN)
        {
            bookAuthor _bookAuthorObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM BOOK_AUTHOR WHERE Aid = " + Convert.ToString(Aid) + " AND ISBN = '"+ISBN+"';", connection);

            try
            {
                connection.Open();
                SqlDataReader dar = cmd.ExecuteReader();

                if (dar.Read())
                {
                    _bookAuthorObj = MapAuthor(dar);
                }
            }
            catch (Exception eObj)
            {
                throw eObj;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return _bookAuthorObj;
        }

        public static void dbStoreBookAuthor(bookAuthor ba)
        {
            dbPostData("INSERT INTO BOOK_AUTHOR VALUES ('" + ba.ISBN + "', " + ba.Aid.ToString() + ")");
        }

        public static void dbRemoveBookAuthorByISBN(string isbn)
        {
            dbPostData("DELETE FROM BOOK_AUTHOR WHERE ISBN = '"+isbn+"'");
        }
    }
}
