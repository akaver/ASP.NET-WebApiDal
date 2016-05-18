using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain
{
    public class Person : BaseEntity
    {
        public int PersonId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [MinLength(1, ErrorMessageResourceName = "FieldMinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(255, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.Firstname), ResourceType = typeof(Resources.Domain))]
        public string Firstname { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [MinLength(1, ErrorMessageResourceName = "FieldMinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(255, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.Lastname), ResourceType = typeof(Resources.Domain))]
        public string Lastname { get; set; }


        // demo fields for date/time handling
        // DataType attribute is used mainly for UI only, to render correct elements
        // https://msdn.microsoft.com/en-us/library/ms186724.aspx#DateandTimeDataTypes
        // look also into DBContext, SQL is forced to use certain fieldtypes according to this attribute
        // this is only tested against MS SQL / LocalDb
        // in views, use custom htmlhelpers - Html.DateTimeEditorFor, DateEditorFor, TimeEditorFor

        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }


        [Display(Name = nameof(Resources.Domain.Person_DateTime2), ResourceType = typeof(Resources.Domain))]
        [DataType(DataType.DateTime, ErrorMessageResourceName = "FieldMustBeDataTypeDateTime", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public DateTime DateTime2 { get; set; }

        [Display(Name = nameof(Resources.Domain.Person_Date2), ResourceType = typeof(Resources.Domain))]
        [DataType(DataType.Date, ErrorMessageResourceName = "FieldMustBeDataTypeDate", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public DateTime Date2 { get; set; }

        [Display(Name = nameof(Resources.Domain.Person_Time2), ResourceType = typeof(Resources.Domain))]
        [DataType(DataType.Time, ErrorMessageResourceName = "FieldMustBeDataTypeTime", ErrorMessageResourceType = typeof(Resources.Common), ErrorMessage = null)]
        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public DateTime Time2 { get; set; }


        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }

        public virtual List<Contact> Contacts { get; set; } = new List<Contact>();

        // not mapped properties, just getters
        public string FirstLastname => (Firstname + " " + Lastname).Trim();
        public string LastFirstname => (Lastname + " " + Firstname).Trim();


    }
}
