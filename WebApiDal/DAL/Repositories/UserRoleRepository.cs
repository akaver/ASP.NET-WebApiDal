using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Identity;

namespace DAL.Repositories
{
    public class UserRoleIntRepository :
        UserRoleRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserRoleIntRepository
    {
        public UserRoleIntRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class UserRoleRepository : UserRoleRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserRoleRepository
    {
        public UserRoleRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class UserRoleRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : EFRepository<TUserRole>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public UserRoleRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public TUserRole GetByUserIdAndRoleId(TKey roleId, TKey userId)
        {
            return DbSet.FirstOrDefault(a => a.RoleId.Equals(roleId) && a.UserId.Equals(userId));
        }
    }
}