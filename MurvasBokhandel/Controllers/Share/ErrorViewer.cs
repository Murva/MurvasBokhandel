using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MurvasBokhandel.Controllers.Share
{
    public class ErrorViewer
    {
        public static string Build(ViewDataDictionary viewData)
        {
            string errorBuild = "";
            int x = 0;
            foreach (ModelState modelState in viewData.ModelState.Values)
            {
                if (modelState.Errors.Count > 0)
                {
                    if (x != 0)
                        errorBuild += "<br/>";

                    int y = 0;
                    foreach (ModelError error in modelState.Errors)
                        if (y == 0)
                        {
                            errorBuild += error.ErrorMessage;
                            y = 1;
                        }
                        else
                            errorBuild += ", " + error.ErrorMessage;

                    x = 1;
                }
            }

            return errorBuild;
        }
    }
}