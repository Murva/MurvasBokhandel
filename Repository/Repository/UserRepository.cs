using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository : BaseRepository
    {
        public static user MapUser(SqlDataReader dar)
        {
            return new user() {
                PersonId = dar["PersonId"] as string,
                Email = dar["Email"] as string,
                FirstName = dar["FirstName"] as string,
                LastName = dar["LastName"] as string,
                Address = dar["Address"] as string,
                Telno = dar["Telno"] as string,
                RoleId = Convert.ToInt32(dar["RoleId"])
            };
        }

        private static string dbGetStringField(string query, string field)
        {
            string _str = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader dar = cmd.ExecuteReader();

                if (dar.Read())
                {
                    _str = dar[field] as string;
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

            return _str;
        }

        public static user dbGetUserByPersonId(string personId)
        {
            user _userObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT \"USER\".\"PersonId\", \"Email\", \"FirstName\", \"LastName\", \"Address\", \"Telno\", \"RoleId\" FROM \"USER\" INNER JOIN BORROWER ON \"USER\".PersonId = \"BORROWER\".PersonId WHERE \"USER\".PersonId = '" + personId + "'", connection);

            try
            {
                connection.Open();
                SqlDataReader dar = cmd.ExecuteReader();

                if (dar.Read())
                {
                    _userObj = MapUser(dar);
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

            return _userObj;
        }

        public static user dbGetUserByEmail(string email)
        {
            user _userObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT \"USER\".\"PersonId\", \"Email\", \"FirstName\", \"LastName\", \"Address\", \"Telno\", \"RoleId\" FROM \"USER\" INNER JOIN BORROWER ON \"USER\".PersonId = \"BORROWER\".PersonId WHERE \"USER\".Email = '"+email+"'", connection);

            try
            {
                connection.Open();
                SqlDataReader dar = cmd.ExecuteReader();

                if (dar.Read())
                {
                    _userObj = MapUser(dar);
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

            return _userObj;
        }

        public static role dbGetUserRole(string email)
        {
            role _roleObj = null;

            if (dbUserExists(email))
            {
                string _connectionString = DataSource.getConnectionString("projectmanager");
                SqlConnection connection = new SqlConnection(_connectionString);
                SqlCommand cmd = new SqlCommand("SELECT name FROM \"ROLE\" AS R INNER JOIN \"USER\" AS U ON R.Id = U.RoleId WHERE U.Email = '"+email+"'", connection);

                try
                {
                    connection.Open();
                    SqlDataReader dar = cmd.ExecuteReader();

                    if (dar.Read())
                    {
                        _roleObj = new role();
                        _roleObj.Name = dar["name"] as string;
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
            }

            return _roleObj;
        }

        public static bool dbUserExists(string email)
        {
            return (dbGetStringField("SELECT Email FROM \"USER\" WHERE Email = '" + email + "'", "email") != null ? true : false);
        }

        public static string dbGetPassword(string email)
        {
            return dbGetStringField("SELECT Password FROM \"USER\" WHERE Email = '" + email + "'", "Password");
        }

        public static void dbCreateUser(user u)
        {
            dbPostData("INSERT INTO \"USER\" VALUES ('" + u.PersonId + "','" + u.Email + "','" + u.Password + "', '" + u.RoleId + "');");
        }
        
        public static void dbRemoveUser(string PersonId){
            dbPostData("DELETE FROM \"USER\" WHERE PersonId = '" + PersonId + "';");
        }
    }
}
