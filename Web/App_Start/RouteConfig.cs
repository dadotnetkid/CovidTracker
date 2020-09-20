using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");


            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "repatriates", action = "login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Admin",
                url: "admin/index/{id}",
                defaults: new { controller = "admin", action = "index", id = UrlParameter.Optional }
            );

            routes.MapMvcAttributeRoutes();
        }
    }
}
