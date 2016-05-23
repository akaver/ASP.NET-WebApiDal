using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var foo = new Contact()
            {
                ContactId = 123,
                PersonId = 456 
            };

            var dummyRepo = new DummyRepo<Contact>();

            var keys = dummyRepo.GetId(foo);

            foreach (var key in keys.OrderBy(k => k.Order))
            {
                Console.WriteLine($"Primary key field is {key.PropertyName}:{key.Order} and value is {key.Value}");
            }

        }

    }

    public class EntityKeyInfo
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public int Order { get; set; }

        public EntityKeyInfo()
        {
        }

        public EntityKeyInfo(string propertyName, object value=null, int order=0)
        {
            PropertyName = propertyName;
            Value = value;
            Order = order;
        }
    }

    public class DummyRepo<T> where T : class
    {
        // find out primary keys in object.
        // do not use EF

        // possibilities are
        // object.id
        // object.ObjectnameId
        // attribute based: [Key]
        // attribute based composite: []
        // in case of fluent api - you are fucked

        public List<EntityKeyInfo> GetId(T entity)
        {
            var res = new List<EntityKeyInfo>();

            var className = typeof(T).Name.ToLower();
            var properties = typeof(T).GetProperties();

            foreach (var propertyInfo in properties)
            {
                var order = 0;
                var isKey = false;

                // lets check the [Key] attributes
                object[] attrs = propertyInfo.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr is KeyAttribute)
                    {
                        isKey = true;
                    }

                    var attribute = attr as ColumnAttribute;
                    if (attribute != null)
                    {
                        order = attribute.Order;
                    }
                }

                if (isKey)
                {
                    res.Add(new EntityKeyInfo(propertyInfo.Name, propertyInfo.GetValue(entity, null), order));
                }
            }

            // if key(s) are already found, return
            if (res.Count > 0) return res;

            // no keys yet, check for name
            foreach (var propertyInfo in properties)
            {
                var name = propertyInfo.Name.ToLower();
                if (name.Equals(className + "id") || name.Equals("id"))
                {
                    res.Add(new EntityKeyInfo() { PropertyName = propertyInfo.Name, Value = propertyInfo.GetValue(entity, null) });
                }

            }


            return res;
        }
    }
}
