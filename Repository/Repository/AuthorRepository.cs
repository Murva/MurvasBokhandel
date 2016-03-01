﻿using Repository.EntityModel;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AuthorRepository
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

        private static List<author> dbGetAuthorList(string query)
        {
            List<author> _authList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
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
            return dbGetAuthorList("SELECT * FROM Author ORDER BY "+orderBy+";");
        }

        public static List<author> dbGetAuthors(string orderBy)
        {
            List<author> _authList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM author ORDER BY "+orderBy+";", con);
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

        public static author dbGetAuthor(int aid)
        {
            author _authorObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM author WHERE Aid = " + Convert.ToString(aid) + ";", connection);

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
    }
}
