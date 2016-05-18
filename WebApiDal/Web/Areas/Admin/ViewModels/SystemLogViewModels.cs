using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.ViewModels
{
    public class SystemLogIndexViewModel
    {
        public string LogFileName { get; set; }
        public SelectList LogFileSelectList { get; set; }

        public string LogFileContent { get; set; }
    }
}