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

        private static string dbGetStringField(string query, string field, SqlParameter sp)
        {
            string _str = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(sp);
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
            SqlCommand cmd = new SqlCommand("SELECT \"USER\".\"PersonId\", \"Email\", \"FirstName\", \"LastName\", \"Address\", \"Telno\", \"RoleId\" FROM \"USER\" INNER JOIN BORROWER ON \"USER\".PersonId = \"BORROWER\".PersonId WHERE \"USER\".PersonId = @PERSONID", connection);
            cmd.Parameters.AddWithValue("@PERSONID", personId);

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
            SqlCommand cmd = new SqlCommand("SELECT \"USER\".\"PersonId\", \"Email\", \"FirstName\", \"LastName\", \"Address\", \"Telno\", \"RoleId\" FROM \"USER\" INNER JOIN BORROWER ON \"USER\".PersonId = \"BORROWER\".PersonId WHERE \"USER\".Email = @EMAIL", connection);
            cmd.Parameters.AddWithValue("@EMAIL", email);

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
                SqlCommand cmd = new SqlCommand("SELECT name FROM \"ROLE\" AS R INNER JOIN \"USER\" AS U ON R.Id = U.RoleId WHERE U.Email = @EMAIL", connection);
                cmd.Parameters.AddWithValue("@EMAIL", email);

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
            return (dbGetStringField("SELECT Email FROM \"USER\" WHERE Email = @EMAIL", "email", new SqlParameter("@EMAIL", email)) != null ? true : false);
        }

        public static string dbGetPassword(string email)
        {
            return dbGetStringField("SELECT Password FROM \"USER\" WHERE Email = @EMAIL", "Password", new SqlParameter("@EMAIL", email));
        }

        private static SqlParameter[] _mapUserParameters(user u)
        {
            return new SqlParameter[] { 
                new SqlParameter("@PERSONID", u.PersonId),
                new SqlParameter("@EMAIL", u.Email),
                new SqlParameter("@PASSWORD", u.Password),
                new SqlParameter("@ROLEID", u.RoleId)
            };
        }

        public static void dbCreateUser(user u)
        {
            dbPostData("INSERT INTO \"USER\" VALUES (@PERSONID, @EMAIL, @PASSWORD, @ROLEID);", _mapUserParameters(u));
        }
        
        public static void dbRemoveUser(string PersonId){
            dbPostData("DELETE FROM \"USER\" WHERE PersonId = @PERSONID;", new SqlParameter[] {
                new SqlParameter("@PERSONID", PersonId)
            });
        }
        public static void dbChangePassword(string Password) 
        {
            //här ska det in ett anrop till databsen med det nya lösenordet
        }
        public static void dbUpdateUser(user u)
        {
            //här ska det in ett anrop till databasen som ändrar emailen i databasen.
            //förs vill vi bara se om den fungerar eller inte så vi kör utan string
            
            dbPostData("UPDATE \"USER\" SET Email = @EMAIL, Password = @PASSWORD WHERE PersonId=@PERSONID;", _mapUserParameters(u));
        }
    }
}
