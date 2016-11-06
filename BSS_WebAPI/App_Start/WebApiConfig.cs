using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using BSS_WebAPI.Utils;

namespace BSS_WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.EnableCors();

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new MyDateTimeConverter());

            // add before default api route
            config.Routes.MapHttpRoute(
                    name: "AutoApi",
                    routeTemplate: "BSS_WebAPI.Controllers.Auto/{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

           


            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType)); // Enables token authentication

        }
    }
}
