using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MurvasBokhandel
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Public", action = "Start", id = UrlParameter.Optional }
            );

            routes.MapRoute(

                name: "Book",
                url: "{controller}/{action}/{isbn}",
                defaults: new { controller = "Book", action = "GetBook", isbn = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AuthorAdmin",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AuthorAdmin", action = "Start", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BorrowerAdmin",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "BorrowerAdmin", action = "Start", id = UrlParameter.Optional }
            );

        }
    }
}
