using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class FineRepository : BaseRepository
    {
        public static int dbGetFine(string barcode, string personId)
        {
            int _fine = 0;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            //SqlCommand cmd = new SqlCommand("SELECT * FROM BOOK WHERE ISBN = '" + isbn + "';", con);
            SqlCommand cmd = new SqlCommand("SELECT SUM(Penaltyperday * (DATEDIFF(DAY, ToBeReturnedDate,GETDATE()))) AS 'Avgift' FROM BORROWER, CATEGORY, BORROW, COPY WHERE BORROWER.CategoryId = CATEGORY.CatergoryId AND COPY.Barcode ='" + barcode + "' AND BORROWER.PersonId='" + personId + "' AND BORROW.Barcode = COPY.Barcode AND BORROW.PersonId = '" + personId + "';", con);
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
