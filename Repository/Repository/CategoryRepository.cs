using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class CategoryRepository : BaseRepository<category>
    {
        public static List<category> dbGetCategories()
        {
            return dbGetList("SELECT * FROM CATEGORY", null);
        }

        public static category dbGetCategoryById(int categoryId)
        {
            return dbGet("SELECT * FROM CATEGORY WHERE CatergoryId = @CATEGORYID;", new SqlParameter[] {
                new SqlParameter("@CATEGORYID", categoryId)
            });
        }
 
    }
}
