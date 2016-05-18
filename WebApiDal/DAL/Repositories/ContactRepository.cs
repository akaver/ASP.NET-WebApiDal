using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class ContactRepository: EFRepository<Contact>, IContactRepository
    {
        public ContactRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<Contact> AllforUser(int userId)
        {
            return
                DbSet.Where(u => u.Person.UserId == userId)
                    .Include(p => p.Person)
                    .Include(t => t.ContactType)
                    .OrderBy(p => p.Person.Lastname)
                    .ThenBy(p => p.Person.Firstname)
                    .ToList();
        }

        public Contact GetForUser(int contactId, int userId)
        {
            return DbSet.FirstOrDefault(a => a.ContactId == contactId && a.Person.UserId == userId);
        }
    }
}
