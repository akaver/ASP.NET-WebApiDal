using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Helpers
{
    public static class PagedListHelper
    {
        public static int GetPageSizeFixed(int totalRecordsInList)
        {
            if (totalRecordsInList <= 200)
            {
                return 100;
            }
            if (totalRecordsInList <= 600)
            {
                return 200;
            }
            if (totalRecordsInList <= 1200)
            {
                return 300;
            }
            if (totalRecordsInList <= 2000)
            {
                return 400;
            }
            return 500;
        }
    }
}