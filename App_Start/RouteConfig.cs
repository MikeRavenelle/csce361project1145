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
                name: "getUserId",
                url: "getUserId",
                defaults: new { controller = "Home", action = "getUserId" }
            );

            routes.MapRoute(
               name: "getPhotoByUser",
               url: "getPhotoByUser",
               defaults: new { controller = "Home", action = "getPhotoByUser" }
           );

            routes.MapRoute(
               name: "getLocationsById",
               url: "getLocationsById",
               defaults: new { controller = "Home", action = "getLocationsById" }
           );

            routes.MapRoute(
                name: "addComment",
                url: "addComment",
                defaults: new { controller = "Home", action = "addComment" }
            );

            routes.MapRoute(
                name: "removeComment",
                url: "removeComment",
                defaults: new { controller = "Home", action = "removeComment" }
            );

            routes.MapRoute(
                name: "removePicture",
                url: "removePicture",
                defaults: new { controller = "Home", action = "removePicture" }
            );

            routes.MapRoute(
                name: "getuser",
                url: "getuser",
                defaults: new { controller = "Home", action = "getUser" }
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