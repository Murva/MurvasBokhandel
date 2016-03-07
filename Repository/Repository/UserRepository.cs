using Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository : BaseRepository
    {
        public static user MapAuthor(SqlDataReader dar)
        {
            author authObj = new author();
            authObj.Aid = Convert.ToInt32(dar["Aid"]);
            authObj.FirstName = dar["FirstName"] as string;
            authObj.LastName = dar["LastName"] as string;
            authObj.BirthYear = dar["BirthYear"] as string;

            return authObj;
        }

        public bool dbUserExists(string email)
        {

        }
    }
}
