using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Domain;

namespace Web.ViewModels
{
    public enum Gender
    {
        Male = 1,
        Female,
        Transgender,
        PreviouslyMale,
        PreviouslyFemale,
        Cat,
        Dog
    }

    public class HelperDemoIndexViewModel
    {
        public int DropDownListId { get; set; }
        public SelectList DropDownList { get; set; }

        [Range(1,int.MaxValue)]
        public Gender EnumDropDown { get; set; }

        public bool CheckBox { get; set; }

        public string TextBox { get; set; }

        public string TextArea { get; set; }

        public int[] ListBoxId { get; set; }
        public MultiSelectList ListBoxList { get; set; }

        public int RadioButtonId1 { get; set; }
        public List<Person> RadioButtonList1 { get; set; }

        public int RadioButtonId2 { get; set; }
        public List<Person> RadioButtonList2 { get; set; }


    }
}

