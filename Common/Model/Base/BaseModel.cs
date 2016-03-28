namespace Common.Model.Base
{
    /// <summary>
    /// Gives a possibillity to alert i the views
    /// </summary>
    public class BaseModel
    {
        private string _alert = null;

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
