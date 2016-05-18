using System.Web;
using System.Web.Mvc;

namespace Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //require https in mvc
            filters.Add(new RequireHttpsAttribute());
        }
    }
}