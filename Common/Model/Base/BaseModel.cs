using Common.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.Base
{
    public class BaseModel
    {
        private static string _alert = null;

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

        public static bool IsLoaded()
        {
            return (_alert != null ? true : false);
        }
    }
}
