using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Helpers
{
    public static class SessionHelper
    {
        public static T GetDataFromSession<T>(this HttpSessionStateBase session, string key)
        {
            //https://msdn.microsoft.com/en-us/library/xwth0h0d.aspx?f=255&MSPPError=-2147217396
            //will return null for reference types and zero for numeric value types. 
            //For structs, it will return each member of the struct initialized to zero or null depending on whether they are value or reference types. 
            //For nullable value types, default returns a System.Nullable<T>, which is initialized like any struct.

            if (session[key] == null)
                return default(T);

            return (T) session[key];
        }

        public static void SetDataToSession<T>(this HttpSessionStateBase session, string key, object value)
        {
            session[key] = value;
        }
    }
}