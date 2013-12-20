using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ninesky
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Category",
                url: "Category/{id}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Ninesky.Controllers" }
                );
            routes.MapRoute(
                name: "Items",
                url: "Items/{id}",
                defaults: new { controller = "CommonModel", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{page}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, page = UrlParameter.Optional },
                namespaces: new string[] { "Ninesky.Controllers" }
                );
        }
    }
}