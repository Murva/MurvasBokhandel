using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class CopyRepository : BaseRepository
    {
        public static copy dbMapCopy(SqlDataReader dar)
        {
            return new copy()
            {
                Barcode = dar["Barcode"] as string,
                ISBN = dar["ISBN"] as string,
                library = dar["library"] as string,
                Location = dar["Location"] as string,
                StatusId = (int)dar["StatusId"]
            };
            
        }
        static public copy dbGetBookCopy(string Barcode){
            copy _copy = new copy();
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand("SELECT * FROM COPY WHERE Barcode = @BARCODE;", con);
            cmd.Parameters.AddWithValue("@BARCODE", Barcode);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    if (dar.Read()) {
                        _copy = dbMapCopy(dar);
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

        private static List<copy> dbGetCopiesList(string query, SqlParameter[] sp)
        {
            List<copy> _copyList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _copyList = new List<copy>();
                    while (dar.Read())
                    {

                        _copyList.Add(dbMapCopy(dar));

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
            return _copyList;
        }

        private static string getNextBarcode()
        {
            string _barcode = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Barcode FROM COPY ORDER BY Barcode DESC;", con);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    if (dar.Read())
                    {
                        _barcode = dar["Barcode"] as string;
                        long bcode = Convert.ToInt64(_barcode);
                        bcode += 1;
                        _barcode = bcode.ToString();
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

            return _barcode;
        }

        public static List<copy> dbGetCopiesByISBN(string ISBN)
        {
            return dbGetCopiesList("SELECT * FROM COPY WHERE ISBN = @ISBN", new SqlParameter[] {
                new SqlParameter("@ISBN", ISBN)
            });
        }

        public static void dbRemoveCopy(string Barcode)
        {
            dbPostData("DELETE FROM COPY WHERE Barcode = @BARCODE", new SqlParameter[] {
                new SqlParameter("@BARCODE", Barcode)
            });
        }

        public static void dbCreateCopy(string isbn, string library)
        {
            dbPostData("INSERT INTO COPY VALUES (@BARCODE, null, 1, @ISBN, @LIBRARY)", new SqlParameter[] {
                new SqlParameter("@BARCODE", getNextBarcode()),
                new SqlParameter("@ISBN", isbn),
                new SqlParameter("@LIBRARY", library)
            });
        }
    }
}
