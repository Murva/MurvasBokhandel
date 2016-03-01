using Repository.EntityModel;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class ClassificationRepository
    {
        private static classification mapClassification(SqlDataReader dar)
        {
            classification c = new classification();
            c.Description = dar["Description"] as string;
            c.SignId = Convert.ToInt32(dar["SignId"]);
            c.Signum = dar["Signum"] as string;

            return c;
        }

        private static List<classification> dbGetClassificationList(string query)
        {
            List<classification> _classificationList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _classificationList = new List<classification>();
                    while (dar.Read())
                    {
                        _classificationList.Add(mapClassification(dar));
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
            return _classificationList;
        }

        public static List<classification> dbGetClassifications()
        {
            return dbGetClassificationList("SELECT * FROM CLASSIFICATION");
        }
    }
}
