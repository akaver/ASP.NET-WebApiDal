using System.Collections.Generic;
using Domain.Identity;
using Microsoft.AspNet.Identity;

namespace Interfaces.Repositories
{
    public interface IRoleIntRepository : IRoleRepository<int, RoleInt>
    {
    }

    public interface IRoleRepository : IRoleRepository<string, Role>
    {
    }

    public interface IRoleRepository<in TKey, TRole> : IBaseRepository<TRole>
        where TRole : class, IRole<TKey>
    {
        TRole GetByRoleName(string roleName);
        List<TRole> GetRolesForUser(TKey userId);
    }
}