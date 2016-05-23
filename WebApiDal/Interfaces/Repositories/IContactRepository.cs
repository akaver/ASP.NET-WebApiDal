using System.Collections.Generic;
using Domain;

namespace Interfaces.Repositories
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        List<Contact> AllforUser(int userId);
        Contact GetForUser(int contactId, int userId);
    }
}
