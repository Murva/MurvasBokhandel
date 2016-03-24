using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Share
{
    public static class Auth
    {
        public static BorrowerWithUser LoggedInUser {get; set;}

        public static bool HasAdminPermission()
        {
            if (HasUserPermission() && LoggedInUser.User.RoleId == 2)
                return true;

            return false;
        }

        public static bool HasUserPermission()
        {
            if (LoggedInUser != null && LoggedInUser.User.RoleId >= 1)
                return true;

            return false;
        }
    }
}
