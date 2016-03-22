using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class BorrowRepository : BaseRepository<borrow>
    {
        static public borrow dbGetBorrowByPersonId(string id) 
        {
            return dbGet("SELECT * FROM BORROW WHERE PersonId = @PERSONID AND ReturnDate IS NULL;", new SqlParameter[] {
                new SqlParameter("@PERSONID", id)
            });
        }

        static public List<borrow> dbGetBorrowListByPersonId(string id)
        {
            return dbGetList("SELECT * FROM BORROW WHERE PersonId = @PERSONID;", new SqlParameter[] {
                new SqlParameter("@PERSONID", id)
            });
        }

        public static List<borrow> dbGetBorrowListByISBN(string isbn)
        {
            return dbGetList("SELECT * FROM BORROW AS B, COPY AS C WHERE B.Barcode = C.Barcode AND C.ISBN = @ISBN", new SqlParameter[] {
                new SqlParameter("@ISBN", isbn)
            });
        }

        public static List<borrow> dbGetBorrowListByBarcode(string Barcode)
        {
            return dbGetList("SELECT * FROM BORROW WHERE Barcode = @BARCODE", new SqlParameter[] {
                new SqlParameter("@BARCODE", Barcode)
            });
        }

        public static void updateDate(borrow b)
        {
            //dbPostData("UPDATE BORROW SET BorrowDate = '" + b.BorrowDate.ToString() + "', ToBeReturnedDate = '" + b.ToBeReturnedDate.ToString() + "' WHERE (Barcode = '" + b.Barcode + "' AND PersonId = '" +b.PersonId+"')");
        }

        public static void dbRemoveBorrowsByPersonId(string PersonId)
        {
            //dbPostData("DELETE FROM BORROW WHERE PersonId = '" + PersonId + "';");
        }
    }
}
