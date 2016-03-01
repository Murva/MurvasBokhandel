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

        public static void dbRemoveBookAuthorByISBN(string isbn)
        {
            dbPostData("DELETE FROM BOOK_AUTHOR WHERE ISBN = "+isbn);
        }
    }
}
