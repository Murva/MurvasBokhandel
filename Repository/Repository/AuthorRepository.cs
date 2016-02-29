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
    public class AuthorRepository
    {
        public static List<author> dbGetAuthors()
        {
            List<author> _authList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM author;", con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _authList = new List<author>();
                    while (dar.Read())
                    {
                        author authObj = new author();
                        authObj.Aid = Convert.ToInt32(dar["Aid"]);
                        authObj.FirstName = dar["FirstName"] as string;
                        authObj.LastName = dar["LastName"] as string;
                        authObj.BirthYear = dar["BirthYear"] as string;
                        _authList.Add(authObj);
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
            return _authList;
        }

        public static author dbGetAuthor(int aid)
        {
            author _authorObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM author WHERE Aid = " + Convert.ToString(aid) + ";", connection);

            try
            {
                connection.Open();
                SqlDataReader dar = cmd.ExecuteReader();

                if (dar.Read())
                {
                    _authorObj = new author();
                    _authorObj.Aid = aid;
                    _authorObj.FirstName = dar["FirstName"].ToString();
                    _authorObj.LastName = dar["LastName"].ToString();
                    _authorObj.BirthYear = dar["BirthYear"].ToString();
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

            return _authorObj;
        }
    }
}
