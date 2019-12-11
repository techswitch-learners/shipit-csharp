using System.Web.Http;
using ShipIt.Filters;

namespace ShipIt
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            config.Routes.MapHttpRoute(
                "ApiRoot",
                "Test/{id}",
                new { controller = "Product", id = RouteParameter.Optional });
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Filters.Add(new ShipItExceptionFilterAttribute());
        }
    }
}