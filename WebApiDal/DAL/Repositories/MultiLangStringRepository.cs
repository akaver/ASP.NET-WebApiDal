using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    public class MultiLangStringRepository : EFRepository<MultiLangString>, IMultiLangStringRepository
    {
        public MultiLangStringRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
