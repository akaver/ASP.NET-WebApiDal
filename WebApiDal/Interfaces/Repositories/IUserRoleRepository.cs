using Domain.Identity;

namespace Interfaces.Repositories
{
    public interface IUserRoleIntRepository : IUserRoleRepository<int, UserRoleInt>
    {
    }

    public interface IUserRoleRepository : IUserRoleRepository<string, UserRole>
    {
    }

    public interface IUserRoleRepository<in TKey, TUserRole> : IBaseRepository<TUserRole>
        where TUserRole : class
    {
        TUserRole GetByUserIdAndRoleId(TKey roleId, TKey userId);
    }
}