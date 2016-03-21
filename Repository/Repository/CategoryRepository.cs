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
    public class CategoryRepository
    {
        public static List<category> dbGetCategories() {
            List<category> categories = new List<category>();

            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM CATEGORY", con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    while (dar.Read())
                    {
                        category c = new category();
                        
                        c.Category = dar["Category"] as string;
                        c.Period = Convert.ToByte(dar["Period"]);
                        c.Penaltyperday = Convert.ToInt32(dar["Penaltyperday"]);
                        c.CatergoryId = Convert.ToInt32(dar["CatergoryId"]);

                        categories.Add(c);
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

            return categories;
        }
        public static category dbGetCategory(int categoryId)
        {

            category _category = new category();
            
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            // ' ' behövdes för att id skulle ses som string
            SqlCommand cmd = new SqlCommand("SELECT * FROM CATEGORY WHERE CatergoryId = @CATEGORYID;", con);
            cmd.Parameters.AddWithValue("@CATEGORYID", categoryId);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar.Read())
                {
                    _category.Category = dar["Category"] as string;
                    
                    _category.Penaltyperday = (int)dar["Penaltyperday"];
                    _category.CatergoryId = Convert.ToInt32(dar["CatergoryId"]);
                    _category.Period = Convert.ToInt32(dar["Period"]);
                    
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

            return _category;
        }
    }
}
