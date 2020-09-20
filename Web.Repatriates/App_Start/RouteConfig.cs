using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Repatriates
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
                defaults: new { controller = "Tracker", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapMvcAttributeRoutes();
        }
    }
}
