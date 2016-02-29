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
    public class BookRepository
    {
        public static List<book> dbGetBookList(string query)
        {
            List<book> _bookList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _bookList = new List<book>();
                    while (dar.Read())
                    {
                        book bookObj = new book();
                        bookObj.ISBN = dar["ISBN"] as string;
                        bookObj.pages = Convert.ToInt32(dar["Pages"]);
                        bookObj.Title = dar["Title"] as string;
                        bookObj.publicationinfo = dar["publicationinfo"] as string;
                        bookObj.PublicationYear = dar["PublicationYear"] as string;
                        bookObj.SignId = Convert.ToInt32(dar["SignId"]);

                        _bookList.Add(bookObj);
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

            return _bookList;
        }

        public static List<book> dbGetBookListByAuthor(int aid, string orderby = "Title")
        {
            return dbGetBookList("SELECT * FROM BOOK as B, BOOK_AUTHOR as BA WHERE B.ISBN = BA.ISBN AND BA.Aid = "+aid.ToString()+" ORDER BY B."+orderby+";");
        }
        public static List<book> dbGetBooks()
        {
            return dbGetBookList("SELECT * FROM BOOK");
        }
    }
}
