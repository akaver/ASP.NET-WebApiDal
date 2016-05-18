using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Web.Filters
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "HTTPS Required"
                };
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }


    public class RequireHttpsAttributeVer2 : AuthorizationFilterAttribute
    {
        public int Port { get; set; }

        public RequireHttpsAttributeVer2()
        {
            Port = 443;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                var response = new HttpResponseMessage();

                if (request.Method == HttpMethod.Get || request.Method == HttpMethod.Head)
                {
                    var uri = new UriBuilder(request.RequestUri)
                    {
                        Scheme = Uri.UriSchemeHttps,
                        Port = this.Port
                    };

                    response.StatusCode = HttpStatusCode.Found;
                    response.Headers.Location = uri.Uri;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.Forbidden;
                }

                actionContext.Response = response;
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }

}