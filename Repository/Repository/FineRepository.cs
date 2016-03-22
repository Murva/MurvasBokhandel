using Repository.Repositories;
using System;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class FineRepository
    {
        public static int dbGetFine(string barcode, string personId)
        {
            int _fine = 0;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            //SqlCommand cmd = new SqlCommand("SELECT * FROM BOOK WHERE ISBN = '" + isbn + "';", con);
            SqlCommand cmd = new SqlCommand("SELECT SUM(Penaltyperday * (DATEDIFF(DAY, ToBeReturnedDate,GETDATE()))) AS 'Avgift' FROM BORROWER, CATEGORY, BORROW, COPY WHERE BORROWER.CategoryId = CATEGORY.CatergoryId AND COPY.Barcode = @BARCODE AND BORROWER.PersonId= @PERSONID AND BORROW.Barcode = COPY.Barcode AND BORROW.PersonId = @PERSONID;", con);
            cmd.Parameters.AddWithValue("@BARCODE", barcode);
            cmd.Parameters.AddWithValue("@PERSONID", personId);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar.Read())
                {
                    _fine = (int)dar["Avgift"];
                    if (_fine <= 0)
                        _fine = 0;
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

            return _fine;
           
        }
    }
}
