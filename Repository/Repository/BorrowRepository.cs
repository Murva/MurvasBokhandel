﻿using Repository.EntityModel;
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
        static public borrow dbGetBorrow(string id){
            borrow _borrow = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            SqlCommand cmd = new SqlCommand("SELECT * FROM BORROW WHERE PersonId = '" + id + "';", con);
            try
            {
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null) {
                    con.Open();                    
                    _borrow = new borrow();
                    _borrow.PersonId = dar["PersonId"] as string;
                    _borrow.ReturnDate = (DateTime)dar["ReturnDate"];
                    _borrow.ToBeReturnedDate = (DateTime)dar["ToBeReturnedDate"];
                    _borrow.BorrowDate = (DateTime)dar["BorrowDate"];
                    _borrow.Barcode = dar["Barcode"] as string;
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
            List<borrow> _borrowList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            SqlCommand cmd = new SqlCommand("SELECT * FROM BORROW WHERE PersonId = '" + id + "';", con);
            try
            {
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null) {
                    con.Open();                    
                    while (dar.Read())
                    {
                        borrow borrow = new borrow();
                        borrow.PersonId = dar["PersonId"] as string;
                        borrow.ReturnDate = (DateTime)dar["ReturnDate"];
                        borrow.ToBeReturnedDate = (DateTime)dar["ToBeReturnedDate"];
                        borrow.BorrowDate = (DateTime)dar["BorrowDate"];
                        borrow.Barcode = dar["Barcode"] as string;

                        _borrowList.Add(borrow);
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
    }
}
