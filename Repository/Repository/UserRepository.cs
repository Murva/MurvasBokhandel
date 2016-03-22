using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Repository.Repository
{
    public class UserRepository : BaseRepository<user>
    {
        public static user dbGetUserByPersonId(string personId)
        {
            return dbGet("SELECT * FROM \"USER\" WHERE PersonId = @PERSONID", new SqlParameter[] {
                new SqlParameter("@PERSONID", personId)
            });
        }

        public static user dbGetUserByEmail(string email)
        {
            return dbGet("SELECT * FROM \"USER\" WHERE Email = @EMAIL", new SqlParameter[]
            {
                new SqlParameter("@EMAIL", email)
            });
        }

        public static bool dbUserExists(string email)
        {
            return (dbGetProperty("SELECT * FROM \"USER\" WHERE Email = @EMAIL", "Email", new SqlParameter[] {
                new SqlParameter("@EMAIL", email)
            }) != null ? true : false);
        }

        public static string dbGetPassword(string email)
        {
            return dbGet("SELECT * FROM \"USER\" WHERE Email = @EMAIL", new SqlParameter[] {
                new SqlParameter("@EMAIL", email)
            }).Password;
        }

        private static SqlParameter[] _mapUserParameters(user u)
        {
            return new SqlParameter[] { 
                new SqlParameter("@PERSONID", u.PersonId),
                new SqlParameter("@EMAIL", u.Email),
                new SqlParameter("@PASSWORD", u.Password),
                new SqlParameter("@ROLEID", u.RoleId)
            };
        }

        public static void dbCreateUser(user u)
        {
            dbPost("INSERT INTO \"USER\" VALUES (@PERSONID, @EMAIL, @PASSWORD, @ROLEID);", _mapUserParameters(u));
        }
        
        public static void dbRemoveUser(string PersonId){
            dbPost("DELETE FROM \"USER\" WHERE PersonId = @PERSONID;", new SqlParameter[] {
                new SqlParameter("@PERSONID", PersonId)
            });
        }
        
        public static void dbChangePassword(string Password) 
        {
            //här ska det in ett anrop till databsen med det nya lösenordet
        }
        
        public static void dbUpdateUser(user u)
        {
            //här ska det in ett anrop till databasen som ändrar emailen i databasen.
            //förs vill vi bara se om den fungerar eller inte så vi kör utan string

            dbPost("UPDATE \"USER\" SET Email = @EMAIL, Password = @PASSWORD WHERE PersonId=@PERSONID;", _mapUserParameters(u));
        }
    }
}
