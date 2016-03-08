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
    public class RoleRepository : BaseRepository
    {
        public static List<role> dbGetRoles() { 
            List<role> roles = new List<role>();

            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            SqlCommand cmd = new SqlCommand("SELECT * FROM \"ROLE\"", con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    while (dar.Read())
                    {
                        role r = new role();
                        r.Id = Convert.ToInt32(dar["Id"]);
                        r.Name = dar["Name"] as string;
                        roles.Add(r);
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

            return roles;
        }
    }
}
