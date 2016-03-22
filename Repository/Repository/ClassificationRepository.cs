using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class ClassificationRepository : BaseRepository<classification>
    {
        public static List<classification> dbGetClassifications()
        {
            return dbGetList("SELECT * FROM CLASSIFICATION", null);
        }
    }
}
