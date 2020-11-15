using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _0506Work_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //萬用Route
            /*routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );*/

            //最初執行時
            routes.MapRoute(
                name: "/",
                url: "",
                defaults: new { Controller = "Home", Action = "NFU" }
            );

            //NFU
            routes.MapRoute(
                name:"NFU",
                url:"NFU",
                defaults:new {Controller = "Home",Action = "NFU"}
            );

            //Download
            routes.MapRoute(
                name: "Download",
                url: "NFU/Download",
                defaults: new { Controller = "Home", Action = "Download" }
            );
        }
    }
}
