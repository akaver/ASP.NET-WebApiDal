using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Identity;

namespace DAL.Repositories
{
    public class UserIntRepository : UserRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>,
        IUserIntRepository
    {
        public UserIntRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class UserRepository : UserRepository<string, Role, User, UserClaim, UserLogin, UserRole>, IUserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class UserRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : EFRepository<TUser>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public UserRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public TUser GetUserByUserName(string userName)
        {
            return DbSet.FirstOrDefault(a => a.UserName.ToUpper() == userName.ToUpper());
        }

        public TUser GetUserByEmail(string email)
        {
            return DbSet.FirstOrDefault(a => a.Email.ToUpper() == email.ToUpper());
        }

        public bool IsInRole(TKey userId, string roleName)
        {
            return DbSet.Find(userId).Roles.Any(a => a.Role.Name == roleName);
        }

        public void AddUserToRole(TKey userId, string roleName)
        {
        }
    }
}