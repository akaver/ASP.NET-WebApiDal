using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace DAL.Interfaces
{
    public interface IUserRoleIntRepository : IUserRoleRepository<int, UserRoleInt>
    {
    }

    public interface IUserRoleRepository : IUserRoleRepository<string, UserRole>
    {
    }

    public interface IUserRoleRepository<in TKey, TUserRole> : IEFRepository<TUserRole>
        where TUserRole : class
    {
        TUserRole GetByUserIdAndRoleId(TKey roleId, TKey userId);
    }
}