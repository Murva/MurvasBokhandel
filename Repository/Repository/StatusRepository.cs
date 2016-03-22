using Repository.EntityModel;
using System.Data.SqlClient;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using Repository.Repositories;

namespace Repository.Repository
{
    public class StatusRepository : BaseRepository<Status>
    {
        public static Status dbGetStatusByStatusId(int statusId)
        {
            return dbGet("SELECT * FROM STATUS WHERE statusId = @STATUSID;", new SqlParameter[] {
                new SqlParameter("@STATUSID", statusId)
            });
        }

        public static Status dbGetStatusByISBN(string isbn)
        {
            return dbGet("SELECT * FROM COPY AS C, STATUS AS S WHERE C.StatusId = S.id AND C.ISBN = @ISBN", new SqlParameter[] {
                new SqlParameter("@ISBN", isbn)
            });
        }
    }
}
