using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class BorrowerRepository : BaseRepository<borrower>
    {
        public static borrower dbGetBorrower(string PersonId)
        {
            return dbGet("SELECT * FROM BORROWER WHERE PersonId = @PERSONID;", new SqlParameter[] {
                new SqlParameter("@PERSONID", PersonId)
            });
        }

        public static List<borrower> dbGetBorrowers()
        {
            return dbGetList("SELECT * FROM BORROWER;", null);
        }

        public static void dbRemoveBorrower(borrower b)
        {
            dbPost("DELETE FROM BORROWER WHERE PersonId = @PERSONID;", new SqlParameter[] {
                new SqlParameter("@PERSONID", b.PersonId)
            });
        }

        public static void dbUpdateBorrower(borrower b)
        {
            dbPost("UPDATE BORROWER SET FirstName = @FIRTNAME, LastName = @LASTNAME, Telno = @TELNO, Address = @ADDRESS WHERE PersonId = @PERSONID", new SqlParameter[] {
                new SqlParameter("@FIRSTNAME", b.FirstName),
                new SqlParameter("@LASTNAME", b.LastName),
                new SqlParameter("@TELNO", b.Telno),
                new SqlParameter("@ADDRESS", b.Address),
                new SqlParameter("@PERSONID", b.PersonId)
            });
        }

        public static void dbStoreBorrower(borrower b)
        {
            //dbPost("INSERT INTO BORROWER VALUES ('"+b.PersonId+"','"+b.FirstName+"','"+b.LastName+"', '"+b.Address+"', '"+b.Telno+"', '"+b.CategoryId+"');");
        }

        public static List<borrower> dbGetBorrowersByLetter(string letter)
        {
            return dbGetList("SELECT * FROM Borrower WHERE LastName LIKE @LETTER+'%';",
                new SqlParameter[] {
                    new SqlParameter("@LETTER", letter)
            });
        }
    }
}
