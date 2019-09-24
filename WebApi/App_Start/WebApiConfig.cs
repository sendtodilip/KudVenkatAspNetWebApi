using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using WebApi.Custom;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultRoute",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Custom Controller Selector
            config.Services.Replace(typeof(IHttpControllerSelector), new CustomControllerSelector(config));

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.pragimtech.students.v1+json"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.pragimtech.students.v2+json"));
            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.pragimtech.students.v1+xml"));
            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.pragimtech.students.v2+xml"));



            //config.Routes.MapHttpRoute(
            //     name: "Version1",
            //     routeTemplate: "api/v1/students/{id}",
            //     defaults: new { id = RouteParameter.Optional, controller = "StudentsV1" }
            // );

            //config.Routes.MapHttpRoute(
            //     name: "Version2",
            //     routeTemplate: "api/v2/students/{id}",
            //     defaults: new { id = RouteParameter.Optional, controller = "StudentsV2" }
            // );

        }
    }
}
