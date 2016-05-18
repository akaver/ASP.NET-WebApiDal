using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IBaseEntity
    {
        DateTime CreatedAtDT { get; set; }
        string CreatedBy { get; set; }
        DateTime ModifiedAtDT { get; set; }
        string ModifiedBy { get; set; }
    }

    public abstract class BaseEntity : IBaseEntity
    {
        public DateTime CreatedAtDT { get; set; }
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        public DateTime ModifiedAtDT { get; set; }
        [MaxLength(256)]
        public string ModifiedBy { get; set; }
    }


}
