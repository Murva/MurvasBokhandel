using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class BorrowRepository : BaseRepository
    {
        static public borrow MapBorrow(SqlDataReader dar)
        {
            borrow _borrow = new borrow();
            _borrow.PersonId = dar["PersonId"] as string;
            _borrow.ReturnDate = (DateTime)dar["ReturnDate"];
            _borrow.ToBeReturnedDate = (DateTime)dar["ToBeReturnedDate"];
            _borrow.BorrowDate = (DateTime)dar["BorrowDate"];
            _borrow.Barcode = dar["Barcode"] as string;
            return _borrow;
        }

        static public borrow dbGetBorrow(string id){
            borrow _borrow = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            SqlCommand cmd = new SqlCommand("SELECT * FROM BORROW WHERE PersonId = '" + id + "';", con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null) {
                    _borrow = MapBorrow(dar);
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
            return _borrow;
        }
        static public List<borrow> dbGetBorrowList(string id)
        {
            List<borrow> _borrowList = new List<borrow>();
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            SqlCommand cmd = new SqlCommand("SELECT * FROM BORROW WHERE PersonId = '" + id + "';", con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null) {                
                    while (dar.Read())
                    {
                        _borrowList.Add(MapBorrow(dar));
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
            return _borrowList;
        }
        public static void updateDate(borrow b){
            dbPostData("UPDATE BORROW SET BorrowDate = '" + b.BorrowDate.ToString() + "', ToBeReturnedDate = '" + b.ToBeReturnedDate.ToString() + "' WHERE (Barcode = '" + b.Barcode + "' AND PersonId = '" +b.PersonId+"')");
        }

        private static void dbPostData(string query)
        {
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public static void dbRemoveBorrowsByPersonId(string PersonId)
        {
            dbPostData("DELETE FROM BORROW WHERE PersonId = '" + PersonId + "';");
        }
    }
}
