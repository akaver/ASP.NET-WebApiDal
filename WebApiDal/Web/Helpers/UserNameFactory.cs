using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Provider;

namespace Web.Helpers
{
    public static class UserNameFactory
    {
        public static Func<string> GetCurrentUserNameFactory() =>
            () => HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.Request.IsAuthenticated
                ? HttpContext.Current.User.Identity.Name
                : "Anonymous";
    }
}