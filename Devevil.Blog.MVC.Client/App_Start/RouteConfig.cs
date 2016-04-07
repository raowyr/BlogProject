using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Devevil.Blog.MVC.Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "ListTopRoute",
              url: "ListTop/{page}",
              defaults: new { controller = "Page", action = "ListTopPages", page = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "ListRoute",
              url: "List/{category}/{idCategory}/{page}",
              defaults: new { controller = "Page", action = "List", category = UrlParameter.Optional, idCategory = UrlParameter.Optional, page = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "ArchiveRoute",
              url: "Archive/{page}/{id}",
              defaults: new { controller = "Page", action = "Index", page = UrlParameter.Optional, id = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

          
        }
    }
}