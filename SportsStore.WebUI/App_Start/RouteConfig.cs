using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");




            /// Empty query with an option for ID.
            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Product", action = "List",category = (string) null, page = 1}
            );

            /// Query with Page and an opion for ID
            routes.MapRoute(
                name: null,
                url: "Pages{page}",
                defaults: new { controller = "Product", action = "List", category = (string)null, page = 1},
                constraints: new { page = @"\d+"}
            );

            /// query with category and an option for ID
            routes.MapRoute(
                name: null,
                url: "{category}",
                defaults: new { controller = "Product", action = "List", page = 1}
            );

            /// Full thing so Category with page and an option for ID
            routes.MapRoute(
                name: null,
                url: "{category}/Pages{page}",
                defaults: new { controller = "Product", action = "List"},
                constraints: new { page = @"\d+"}
            );

            //routes.MapRoute(
            //    name: null,
            //    url: "{controller}/{action}/Pages{page}/{id}",
            //    defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
