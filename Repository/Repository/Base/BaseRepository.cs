﻿using Repository.Repositories;
using System;
using System.Data.SqlClient;

namespace Repository.Repository.Base
{
    public class BaseRepository
    {
        protected static void dbPostData(string query)
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
    }
}
