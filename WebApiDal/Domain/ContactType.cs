using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ContactType : BaseEntity
    {
        public int ContactTypeId { get; set; }


        [ForeignKey(nameof(ContactTypeName))]
        public int ContactTypeNameId { get; set; }  
        public virtual MultiLangString ContactTypeName { get; set; }


        public virtual List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
