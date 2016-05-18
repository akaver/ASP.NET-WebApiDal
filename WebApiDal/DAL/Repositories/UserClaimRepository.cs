using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Identity;

namespace DAL.Repositories
{
    public class UserClaimIntRepository :
        UserClaimRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserClaimIntRepository
    {
        public UserClaimIntRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class UserClaimRepository : UserClaimRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserClaimRepository
    {
        public UserClaimRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class UserClaimRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : EFRepository<TUserClaim>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public UserClaimRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<TUserClaim> AllIncludeUser()
        {
            return DbSet.Include(a => a.User).ToList();
        }
    }
}