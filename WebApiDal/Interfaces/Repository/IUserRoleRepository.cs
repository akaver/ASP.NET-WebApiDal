using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Interfaces.Repository
{
    public interface IUserRoleIntRepository : IUserRoleRepository<int, UserRoleInt>
    {
    }

    public interface IUserRoleRepository : IUserRoleRepository<string, UserRole>
    {
    }

    public interface IUserRoleRepository<in TKey, TUserRole> : IRepository<TUserRole>
        where TUserRole : class
    {
        TUserRole GetByUserIdAndRoleId(TKey roleId, TKey userId);
    }
}