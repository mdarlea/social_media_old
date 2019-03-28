using System.Net.Http.Formatting;
using System.Web.Http;

namespace Swaksoft.Communicator.Host
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{action}.{ext}/{id}",
                    defaults: new
                    {
                        id = RouteParameter.Optional,
                        ext = RouteParameter.Optional
                    });

            config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");
            config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "text/xml");
        }
    }
}
