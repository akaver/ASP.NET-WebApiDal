using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;
using Interfaces.Repository;

namespace DAL.Repositories
{
    public class ContactTypeRepository : Repository<ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
