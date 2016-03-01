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
    public class BookAuthorRepository
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
                        author a = new author();
                        a.Aid = (int) dar["Aid"];
                        a.BirthYear = dar["BirthYear"] as string;
                        a.FirstName = dar["FirstName"] as string;
                        a.LastName = dar["LastName"] as string;
                        _authorList.Add(a);
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
    }
}
