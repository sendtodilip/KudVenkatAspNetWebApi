using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApi.Custom
{
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;
        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            _config = config;
        }
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();
            var routeData = request.GetRouteData();
            var controllerName = "students";//routeData.Values["controller"].ToString(); - commented as its returning NULL
            string versionNumber = "1";
            //var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);
            //if (versionQueryString["v"] != null)
            //{
            //    versionNumber = versionQueryString["v"];
            //}

            //string customHeader = "X-StudentService-Version";
            //if (request.Headers.Contains(customHeader))
            //{
            //    versionNumber = request.Headers.GetValues(customHeader).FirstOrDefault();
            //    if (versionNumber.Contains(","))
            //    {
            //        versionNumber = versionNumber.Substring(0, versionNumber.IndexOf(","));
            //    }
            //}

            //Web API versioning using accept Header
            //var acceptHeader = request.Headers.Accept.Where(a => a.Parameters.Count(p=>p.Name.ToLower()=="version") > 0);
            //if (acceptHeader.Any())
            //{
            //    versionNumber = acceptHeader.First().Parameters.First(p => p.Name.ToLower() == "version").Value;
            //}

            //Web API versioning using custom media type
            var regex = @"application\/vnd\.pragimtech\.([a-z]+)\v(?<version>[0-9]+)\+([a-z]+)";
            var acceptHeader = request.Headers.Accept.Where(a => Regex.IsMatch(a.MediaType, regex, RegexOptions.IgnoreCase));
            if (acceptHeader.Any())
            {
                var match = Regex.Match(acceptHeader.First().MediaType, regex, RegexOptions.IgnoreCase);
                versionNumber = match.Groups["version"].Value;
            }

            if (versionNumber == "1")
            {
                controllerName = controllerName + "V1";
            }
            else
            {
                controllerName = controllerName + "V2";
            }
            HttpControllerDescriptor controllerDescriptor;
            if (controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            return null;
        }
    }
}