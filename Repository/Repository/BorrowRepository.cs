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
    public class BorrowRepository
    {
        static public borrow dbGetBorrow(string id)
        {
            borrow _borrowObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            SqlCommand cmd = new SqlCommand("SELECT * FROM BORROW WHERE PersonId = '" + id + "';", con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar.Read())
                {
                    _borrowObj = new borrow();
                    _borrowObj.PersonId = dar["PersonId"] as string;
                    _borrowObj.ReturnDate = (DateTime)dar["ReturnDate"];
                    _borrowObj.ToBeReturnedDate = (DateTime)dar["ToBeReturnedDate"];
                    _borrowObj.BorrowDate = (DateTime)dar["BorrowDate"];
                    _borrowObj.Barcode = dar["Barcode"] as string;
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
            return _borrowObj;
        }
    }
}
