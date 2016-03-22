using Repository.EntityModel;
using Repository.Repositories;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class AuthorRepository : BaseRepository<author>
    {


        public static author MapAuthor(SqlDataReader dar)
        {
            author authObj = new author();
            authObj.Aid = Convert.ToInt32(dar["Aid"]);
            authObj.FirstName = dar["FirstName"] as string;
            authObj.LastName = dar["LastName"] as string;
            authObj.BirthYear = dar["BirthYear"] as string;

            return authObj;
        }

        private static List<author> dbGetAuthorList(string query, SqlParameter[] sp)
        {
            List<author> _authList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _authList = new List<author>();
                    while (dar.Read())
                    {

                        _authList.Add(MapAuthor(dar));

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
            return _authList;
        }

        public static List<author> dbGetAuthors(string orderBy)
        {
            return dbGetAuthorList("SELECT * FROM Author ORDER BY "+orderBy+";", null);
        }


        public static List<author> dbGetAuthorsByLetter(string letter)
        {
            return dbGetAuthorList("SELECT * FROM Author WHERE LastName LIKE @LETTER+'%';",
                 new SqlParameter[] {
                    new SqlParameter("@LETTER", letter)
                 });
        }


        public static List<author> dbGetAuthorsBySearch(string search)
        {
            return dbGetAuthorList("SELECT * FROM Author WHERE FirstName LIKE '%'+@SEARCH+'%' OR LastName LIKE '%'+@SEARCH+'%';",
                 new SqlParameter[] {
                    new SqlParameter("@SEARCH", search)
                 });
        }

        public static author dbGetAuthor(int aid)
        {
            author _authorObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM author WHERE Aid = @AID;", connection);

            cmd.Parameters.Add(new SqlParameter("@AID", aid));

            try
            {
                connection.Open();
                SqlDataReader dar = cmd.ExecuteReader();

                if (dar.Read())
                {
                    _authorObj = MapAuthor(dar);
                }
            }
            catch (Exception eObj)
            {
                throw eObj;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return _authorObj;
        }

        
        private static SqlParameter[] mapAuthorParameters(author a)

        {
            return new SqlParameter[] {
                new SqlParameter() {
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    SqlValue = 50,
                    ParameterName = "@FIRSTNAME",
                    IsNullable = true,
                    Value = a.FirstName
                },
                new SqlParameter("@LASTNAME", a.LastName){
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    SqlValue = 50,
                    ParameterName = "@LASTNAME",
                    IsNullable = true,
                    Value = a.LastName
                },
                new SqlParameter("@BIRTHYEAR", a.BirthYear){
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    SqlValue = 10,
                    ParameterName = "@BIRTHYEAR",
                    IsNullable = true,
                    Value = a.BirthYear == null ? DBNull.Value.ToString() : a.BirthYear
                },
                new SqlParameter() {
                    SqlDbType = System.Data.SqlDbType.Int,
                    ParameterName = "@AID",
                    IsNullable = false,
                    Value = a.Aid
                }
            };
        }

        public static List<author> dbGetAuthors(string orderBy)
        {
            return dbGetList("SELECT * FROM Author ORDER BY "+orderBy+";", null);
        }

        public static List<author> dbGetAuthorsBySearch(string search)
        {
            return dbGetList("SELECT * FROM Author WHERE FirstName LIKE '%'+@SEARCH+'%' OR LastName LIKE '%'+@SEARCH+'%';",
                 new SqlParameter[] {
                    new SqlParameter("@SEARCH", search)
                 });
        }

        public static List<author> dbGetAuthorsByBookISBN(string isbn)
        {
            return dbGetList("SELECT * FROM BOOK_AUTHOR INNER JOIN AUTHOR ON BOOK_AUTHOR.Aid=AUTHOR.Aid WHERE BOOK_AUTHOR.ISBN = @ISBN", new SqlParameter[] {
                new SqlParameter("@ISBN", isbn)
            });
        }

        public static author dbGetAuthor(int aid)
        {
            return dbGet("SELECT * FROM author WHERE Aid = @AID;", new SqlParameter[] {
                new SqlParameter("@AID", aid)
            });
        }

        public static void UpdateAuthor(author a)
        {
            dbPost("UPDATE AUTHOR SET FirstName = @FIRSTNAME, LastName = @LASTNAME, BirthYear = @BIRTHYEAR WHERE Aid = @AID", mapAuthorParameters(a));
        }

        public static void StoreAuthor(author a)
        {
            dbPost("INSERT INTO AUTHOR VALUES (@FIRSTNAME, @LASTNAME, @BIRTHYEAR)", mapAuthorParameters(a));
        }

        public static void DeleteAuthor(author a)
        {
            dbPost("DELETE FROM AUTHOR WHERE Aid = @AID", new SqlParameter[] { 
                new SqlParameter("@AID", a.Aid)
            });
        }
    }
}
