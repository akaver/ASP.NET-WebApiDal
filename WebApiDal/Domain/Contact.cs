using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Contact : BaseEntity
    {
        public int ContactId { get; set; }

        [Required]
        [MaxLength(128, ErrorMessageResourceName = nameof(Resources.Domain.ContactValueLengthError), ErrorMessageResourceType = typeof(Resources.Domain))]
        [MinLength(1, ErrorMessageResourceName = nameof(Resources.Domain.ContactValueLengthError), ErrorMessageResourceType = typeof(Resources.Domain))]
        public string ContactValue { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }

    }
}
