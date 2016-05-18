using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace Web.ViewModels
{
    public class ContactIndexViewModel
    {
        public int ContactCount { get; set; }
        public List<Contact> Contacts { get; set; }
    }

    public class ContactCreateEditViewModel
    {
        public Contact Contact { get; set; }
        public SelectList PersonSelectList { get; set; }
        public SelectList ContactTypeSelectList { get; set; }
    }

}