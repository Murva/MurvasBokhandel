using Repository.EntityModel;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class BorrowerRepository
    {
        private static borrower mapBorrower(SqlDataReader dar)
        {
            borrower _borrowerObj = new borrower();
            _borrowerObj.PersonId = dar["PersonId"].ToString();
            _borrowerObj.FirstName = dar["FirstName"].ToString();
            _borrowerObj.LastName = dar["LastName"].ToString();
            _borrowerObj.Address = dar["Address"].ToString();
            _borrowerObj.CategoryId = Convert.ToInt32(dar["CategoryId"]);
            _borrowerObj.Telno = dar["Telno"].ToString();

            return _borrowerObj;
        }

        public static borrower dbGetBorrower(string PersonId)
        {
            borrower _borrowerObj = new borrower();
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM BORROWER WHERE PersonId = '" + PersonId + "';", connection);

            try
            {
                connection.Open();
                SqlDataReader dar = cmd.ExecuteReader();

                if (dar.Read())
                {
                    _borrowerObj = mapBorrower(dar);
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

            return _borrowerObj;
        }

        public static List<borrower> dbGetBorrowerList(string query)
        {
            List<borrower> _borrowerList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _borrowerList = new List<borrower>();
                    while (dar.Read())
                    {
                        _borrowerList.Add(mapBorrower(dar));
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

            return _borrowerList;
        }

        public static List<borrower> dbGetBorrowers(int aid, string orderby = "Title")
        {
            return dbGetBorrowerList("SELECT * FROM BORROWER;");
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

        public static void dbRemoveBorrower(borrower b)
        {
            dbPostData("DELETE FROM BORROWER WHERE PersonId = " + b.PersonId + ";");
        }

        public static void dbUpdateBorrower(borrower b)
        {
            dbPostData("UPDATE BORROWER SET PersonId = '" + b.PersonId + "', CategoryId = '" + b.CategoryId + "', FirstName = '" + b.FirstName + "', LastName = " + b.LastName + " WHERE Telno = '" + b.Telno + " WHERE Address = '" + b.Address + "';");
        }

        public static void dbStoreBorrower(borrower b)
        {
            dbPostData("INSERT INTO BORROWER VALUES ('"+b.PersonId+"','"+b.FirstName+"','"+b.LastName+"', '"+b.Address+"', '"+b.Telno+"', '"+b.CategoryId+"');");
        }
    }
}
