using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DAL.Helpers
{
    /// <summary>
    /// For strorin info about possible entity primary keys
    /// </summary>
    public class EntityKeyInfo
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public int Order { get; set; }

        public EntityKeyInfo()
        {
        }

        public EntityKeyInfo(string propertyName, object value = null, int order = 0)
        {
            PropertyName = propertyName;
            Value = value;
            Order = order;
        }
    }
}
