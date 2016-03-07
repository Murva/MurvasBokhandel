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
                Password = dar["Password"] as string,
                RoleId = Convert.ToInt32(dar["RoleID"])
            };
        }

        public static user dbGetUser(string query)
        {
            user _userObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, connection);

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

        public static bool dbUserExists(string email)
        {
            user user = dbGetUser("SELECT Email FROM \"USER\" WHERE Email = '"+email+"'");

            return (user != null ? true : false);
        }

        public static bool dbCheckPassword(string email, string password)
        {
            user user = dbGetUser("SELECT Password FROM \"USER\" WHERE Email = '" + email + "'");

            if (user != null)
                if (user.Password == password)
                    return true;

            return false;
        }
    }
}
