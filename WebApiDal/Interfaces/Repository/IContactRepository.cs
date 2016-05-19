using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Interfaces.Repository;

namespace DAL.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        List<Contact> AllforUser(int userId);
        Contact GetForUser(int contactId, int userId);
    }
}
