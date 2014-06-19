using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace csce361project1145
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "getdata",
                url: "getdata",
                defaults: new { controller = "Home", action = "getLocations" }
            );

            routes.MapRoute(
                name: "getPics",
                url: "getPics",
                defaults: new { controller = "Home", action = "getPictures" }
            );

            routes.MapRoute(
                name: "getComments",
                url: "getComments",
                defaults: new { controller = "Home", action = "getComments" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}