using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EPadPw
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Account",
                url: "Account/{action}",
                defaults: new { controller = "Account", action = "Index" }
            );

            routes.MapRoute(
                name: "Notes",
                url: "Notes/{action}/{id}",
                defaults: new { controller = "Notes", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Note",
                url: "{id}",
                defaults: new { controller = "Note", action = "Pad" }
            );

            routes.MapRoute(
                name: "DefaultNote",
                url: "Note/{action}/{id}",
                defaults: new { controller = "Note", action = "Edit", id = UrlParameter.Optional }
            );
        }
    }
}
