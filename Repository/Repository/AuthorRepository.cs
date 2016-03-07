using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class AuthorRepository : BaseRepository
    {

        public static author MapAuthor(SqlDataReader dar)
        {
            author authObj = new author();
            authObj.Aid = Convert.ToInt32(dar["Aid"]);
            authObj.FirstName = dar["FirstName"] as string;
            authObj.LastName = dar["LastName"] as string;
            authObj.BirthYear = dar["BirthYear"] as string;

            return authObj;
        }

        private static List<author> dbGetAuthorList(string query)
        {
            List<author> _authList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _authList = new List<author>();
                    while (dar.Read())
                    {

                        _authList.Add(MapAuthor(dar));

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

        public static List<author> dbGetAuthors(string orderBy)
        {

            return dbGetAuthorList("SELECT * FROM Author ORDER BY " + orderBy + ";");
        }

        public static List<author> dbGetAuthorsBySearch(string search)
        {
            return dbGetAuthorList("SELECT * FROM Author WHERE FirstName LIKE '%" + search + "%' OR LastName LIKE '%" + search + "%';");

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
                    _authorObj = MapAuthor(dar);
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

        public static void UpdateAuthor(author a)
        {
            dbPostData("UPDATE AUTHOR SET FirstName = '"+a.FirstName+"', LastName='"+a.LastName+"', BirthYear='"+a.BirthYear+"' WHERE Aid = "+a.Aid.ToString());
        }

        public static void StoreAuthor(author a)
        {
            dbPostData("INSERT INTO AUTHOR VALUES ('" + a.FirstName + "','" + a.LastName + "','" + a.BirthYear + "')");
        }

        public static void DeleteAuthor(author a)
        {
            dbPostData("DELETE FROM AUTHOR WHERE Aid = "+a.Aid.ToString());
        }
    }
}
