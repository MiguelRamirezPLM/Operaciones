using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{ed}/{ad}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional, ed = UrlParameter.Optional, ad = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default1",
                url: "{controller}/{action}/{id}/{ed}/{ad}/{ud}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional, ed = UrlParameter.Optional, ad = UrlParameter.Optional, ud = UrlParameter.Optional }
            );
        }
    }
}
