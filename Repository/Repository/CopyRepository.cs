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
    public class CopyRepository
    {
        static public copy dbGetBookCopy(string Barcode){
            copy _copy = new copy();
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            SqlCommand cmd = new SqlCommand("SELECT * FROM COPY WHERE Barcode = '" + Barcode + "';", con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    if (dar.Read()) {
                        _copy.Barcode = dar["Barcode"] as string;
                        _copy.ISBN = dar["ISBN"] as string;
                        _copy.library = dar["library"] as string;
                        _copy.Location = dar["Location"] as string;
                        _copy.StatusId = (int)dar["StatusId"];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return _copy;
        }
    }
}
