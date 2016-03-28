using Common.Model;
using Common.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Auth
    {
        private BorrowerWithUser _loggedInUser = null;
        public BorrowerWithUser LoggedInUser {
            get
            {
                return _loggedInUser;
            } 
        }
        private string _alert = null;

        public Auth(BorrowerWithUser b)
        {
            _loggedInUser = b;
        }

        public bool HasAdminPermission()
        {
            if (HasUserPermission() && LoggedInUser.User.RoleId == 2)
                return true;

            return false;
        }

        public bool HasUserPermission()
        {
            if (LoggedInUser != null && LoggedInUser.User.RoleId >= 1)
                return true;

            return false;
        }

        public void PushAlert(string alertView)
        {
            _alert = alertView;
        }

        public string PopAlert()
        {
            string temp = _alert;
            _alert = null;
            return temp;
        }

        public bool IsAlerted()
        {
            return (_alert != null ? true : false);
        }
    }
}
