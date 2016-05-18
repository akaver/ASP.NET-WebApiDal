using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Web.Areas.Admin.ViewModels
{
    public class ContactTypeCreateEditViewModel
    {
        public ContactType ContactType { get; set; }
        public string ContactTypeName { get; set; }
    }


}