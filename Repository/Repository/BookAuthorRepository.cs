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
            SqlCommand cmd = new SqlCommand("SELECT * FROM BOOK_AUTHOR INNER JOIN AUTHOR ON BOOK_AUTHOR.Aid=AUTHOR.Aid WHERE BOOK_AUTHOR.ISBN = @ISBN;", con);
            SqlParameter[] spc = { 
                new SqlParameter("@ISBN", isbn)
            };

            cmd.Parameters.AddRange(spc);

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

            SqlCommand cmd = new SqlCommand("SELECT * FROM BOOK_AUTHOR WHERE Aid = @Aid AND ISBN = '@ISBN';", connection);
            SqlParameter[] spc = { 
                new SqlParameter("@Aid", Aid),
                new SqlParameter("@ISBN", ISBN)
            };

            cmd.Parameters.AddRange(spc);
            
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
            dbPostData("INSERT INTO BOOK_AUTHOR VALUES (@ISBN, @Aid)", new SqlParameter[] {
                new SqlParameter("@ISBN", ba.ISBN),
                new SqlParameter("@Aid", ba.Aid)
            });
        }

        public static void dbRemoveBookAuthorByISBN(string isbn)
        {
            dbPostData("DELETE FROM BOOK_AUTHOR WHERE ISBN = @ISBN", new SqlParameter[] {
                new SqlParameter("@ISBN", isbn)
            });
        }

        public static void dbRemoveBookAuthor(int Aid, string ISBN)
        {
            dbPostData("DELETE FROM BOOK_AUTHOR WHERE ISBN = @ISBN AND Aid = @Aid", new SqlParameter[] {
                new SqlParameter("@ISBN", ISBN),
                new SqlParameter("@Aid", Aid)
            });
        }
    }
}
