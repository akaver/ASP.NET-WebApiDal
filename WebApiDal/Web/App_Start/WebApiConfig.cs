using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var formatters = config.Formatters;
            var jsonFormatter = formatters.JsonFormatter;

            // remove xml formatter
            formatters.Remove(formatters.XmlFormatter);

            // json referenceloop
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            // json referenceloop objects
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            // pretty output
            jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            // response case
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // ignore null objects
            jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            // change the DateTime format
            jsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

            // TimeZone format?
            jsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            // culture of the serializer
            jsonFormatter.SerializerSettings.Culture = new CultureInfo("et-EE");




            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );
        }
    }
}