using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace EmployeeService
{
    public static class WebApiConfig
    {
        public class CustomJsonFormatter : JsonMediaTypeFormatter
        {
            public CustomJsonFormatter()
            {
                this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            }

            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Enable Cors

            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:8080/,http://pragimtech.com","*", "GET,POST");
            //EnableCorsAttribute cors = new EnableCorsAttribute("*","*","*");
            //config.EnableCors(cors);

            //Enabling https for entire web API application
            //config.Filters.Add(new RequireHttpsAttribute());

            //Enable cross domain ajax call thorugh JSONP
            //var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jsonpFormatter);

            //config.Formatters.Add(new CustomJsonFormatter());


            //TO Get json when called from Browser and get the requested type if called from Fiddler or other tools
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            //To get only Xml response
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //To get only Json response
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            //To Get Indented JSON
            //config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            //To Convert Pascal Case to Camel Case
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}
