using System;
using System.Web;

namespace WebApi.Server.Helpers
{
    public static class UserNameFactory
    {
        public static Func<string> GetCurrentUserNameFactory() =>
            () => HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.Request.IsAuthenticated
                ? HttpContext.Current.User.Identity.Name
                : "Anonymous";
    }
}