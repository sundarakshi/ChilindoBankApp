using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Chilindo.CustomerAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new Handlers.APIKeyHandler());
            config.Filters.Add(new Handlers.ValidateModelAttribute());
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
