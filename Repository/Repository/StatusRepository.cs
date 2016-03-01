using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.EntityModel;
using Repository.Repositories;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class StatusRepository
    {
        static public Status dbGetStatus(int statusId) {
            Status _status = null;

            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            SqlCommand cmd = new SqlCommand("SELECT * FROM STATUS WHERE statusId = '" + statusId + "';", con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    if (dar.Read()) {
                        _status = new Status();
                        _status.statusid = (int)dar["statusId"];
                        _status.status = dar["status"] as string;
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

            return _status;
        }
    }
}
