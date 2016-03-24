using Common.Model;
using Common.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Auth
    {
        private static BorrowerWithUser _loggedInUser = null;
        public static BorrowerWithUser LoggedInUser {
            get
            {
                return _loggedInUser;
            } 
        }
        private static string _alert = null;

        public static void Login(BorrowerWithUser b)
        {
            _loggedInUser = b;
        }

        public static void Logout()
        {
            _loggedInUser = null;
        }

        public static void UpdateUser(BorrowerWithUser b)
        {
            _loggedInUser = b;
        }

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

        public static void PushAlert(string alertView)
        {
            _alert = alertView;
        }

        public static string PopAlert()
        {
            string temp = _alert;
            _alert = null;
            return temp;
        }

        public static bool IsAlerted()
        {
            return (_alert != null ? true : false);
        }
    }
}
