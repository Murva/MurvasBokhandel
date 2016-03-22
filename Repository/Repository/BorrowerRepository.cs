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
            dbPost("UPDATE BORROWER SET FirstName = @FIRSTNAME, LastName = @LASTNAME, Telno = @TELNO, Address = @ADDRESS, CategoryId = @CATEGORYID WHERE PersonId = @PERSONID", new SqlParameter[] {
                new SqlParameter("@FIRSTNAME", b.FirstName),
                new SqlParameter("@LASTNAME", b.LastName),
                new SqlParameter() {
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    ParameterName = "@TELNO",
                    IsNullable = true,
                    Value = b.Telno == null ? DBNull.Value.ToString() : b.Telno
                },
                new SqlParameter("@ADDRESS", b.Address),
                new SqlParameter("@PERSONID", b.PersonId),
                new SqlParameter("@CATEGORYID", b.CategoryId)
            });
        }

        public static void dbStoreBorrower(borrower b)
        {
            //dbPost("INSERT INTO BORROWER VALUES ('"+b.PersonId+"','"+b.FirstName+"','"+b.LastName+"', '"+b.Address+"', '"+b.Telno+"', '"+b.CategoryId+"');");

            //dbPost("INSERT INTO AUTHOR VALUES (@FIRSTNAME, @LASTNAME, @BIRTHYEAR)", mapAuthorParameters(a));
            dbPost("INSERT INTO BORROWER VALUES (@PERSONID, @FIRSTNAME, @LASTNAME, @ADDRESS, @TELNO, @CATEGORYID)", new SqlParameter[] {
                new SqlParameter("@PERSONID", b.PersonId),
                new SqlParameter("@FIRSTNAME", b.FirstName),
                new SqlParameter("@LASTNAME", b.LastName),
                new SqlParameter("@ADDRESS", b.Address),
                new SqlParameter() {
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    ParameterName = "@TELNO",
                    IsNullable = true,
                    Value = b.Telno == null ? DBNull.Value.ToString() : b.Telno
                },
                new SqlParameter("@CATEGORYID", b.CategoryId)
            });
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
