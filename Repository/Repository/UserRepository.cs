using Repository.EntityModel;
using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository : BaseRepository
    {
        public static user MapAuthor(SqlDataReader dar)
        {
            return new user() {
                PersonId = dar["PersonId"] as string,
                Email = dar["Email"] as string,
                Password = dar["Password"] as string,
                RoleId = Convert.ToInt32(dar["RoleID"])
            };
        }

        public bool dbUserExists(string email)
        {

        }
    }
}
