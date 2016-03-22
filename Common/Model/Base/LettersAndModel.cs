using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.Base
{
    public class LettersAndModel<T>
        where T : class
    {
        protected List<string> _letters { get; set; }
        protected List<T> _tObj { get; set; }

        public LettersAndModel(List<string> letters, List<T> tObj)
        {
            _letters = letters;
            _tObj = tObj;
        }

        public string Render(string hrefLetter, string hrefProperty, string linkProperty, string[] title) {
            Type t = typeof(T);

            string html = "<div class=\"row\"><ul class=\"list-inline\">";
            
            foreach (var letter in _letters)
                html += "<li><a href=\""+hrefLetter+letter+"\">"+letter+"</a></li>";
                
            html += "</ul></div><div class=\"row\"><ul>";

            foreach (var prop in _tObj)
            {
                string fullTitle = "";

                try
                {
                    foreach (string s in title)
                        fullTitle += t.GetProperty(s).GetValue(prop) + " ";

                    html += "<li><a href=\"" + hrefProperty + t.GetProperty(linkProperty).GetValue(prop) + "\">" + fullTitle + "</a></li>";

                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            html += "</ul></div>";

            return html;
        }
    }
}
