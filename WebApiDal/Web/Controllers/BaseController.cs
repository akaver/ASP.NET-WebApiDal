using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Web.Helpers;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var cookie = Request.Cookies["_culture"];

            string requestCulture = Request["culture"];
            string cookieCulture = cookie?.Value;

            //priority - url, cookie
            string cultureName = requestCulture ?? cookieCulture;

            //validate culture
            cultureName = CultureHelper.GetImplementedUICulture(cultureName);

            if (requestCulture != cookieCulture)
            {
                //update cookie
                if (cookie != null)
                    cookie.Value = cultureName;
                else
                {
                    cookie = new HttpCookie("_culture")
                    {
                        Value = cultureName,
                        Expires = DateTime.Now.AddYears(1)
                    };
                }
                Response.Cookies.Add(cookie);
            }

            // if no cookie, try to get value from header (ie browser preference)
            //else
            //    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
            //    Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
            //                        null;


            // Modify current thread's cultures       
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture =
                new System.Globalization.CultureInfo(
                    CultureHelper.GetCultureForUICulture(Thread.CurrentThread.CurrentUICulture));


            // modify datetime.tostring formating in estonian locale (remove seconds)
            // default format is: ShortDatePattern + ' ' + LongTimePattern
            if (Thread.CurrentThread.CurrentCulture.Name.StartsWith("et"))
            {
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongTimePattern = "HH:mm";
            }

            return base.BeginExecuteCore(callback, state);
        }
    }
}