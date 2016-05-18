using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;
using Microsoft.AspNet.Identity;

namespace DAL.Interfaces
{
    public interface IRoleIntRepository : IRoleRepository<int, RoleInt>
    {
    }

    public interface IRoleRepository : IRoleRepository<string, Role>
    {
    }

    public interface IRoleRepository<in TKey, TRole> : IEFRepository<TRole>
        where TRole : class, IRole<TKey>
    {
        TRole GetByRoleName(string roleName);
        List<TRole> GetRolesForUser(TKey userId);
    }
}