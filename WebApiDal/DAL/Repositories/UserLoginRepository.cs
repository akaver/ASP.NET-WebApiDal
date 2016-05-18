using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using Domain.Identity;

namespace DAL.Repositories
{
    public class UserLoginIntRepository :
        UserLoginRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserLoginIntRepository
    {
        public UserLoginIntRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }

    public class UserLoginRepository : UserLoginRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserLoginRepository
    {
        public UserLoginRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }

    public class UserLoginRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : EFRepository<TUserLogin>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public UserLoginRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public List<TUserLogin> GetAllIncludeUser()
        {
            return DbSet.Include(a => a.User).ToList();
        }

        public TUserLogin GetUserLoginByProviderAndProviderKey(string loginProvider, string providerKey)
        {
            return DbSet.FirstOrDefault(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);
        }
    }
}