using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Identity;
using Microsoft.AspNet.Identity;

namespace DAL.Repositories
{
    public class RoleIntRepository : RoleRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>,
        IRoleIntRepository
    {
        public RoleIntRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class RoleRepository : RoleRepository<string, Role, User, UserClaim, UserLogin, UserRole>, IRoleRepository
    {
        public RoleRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class RoleRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : EFRepository<TRole>,
        IRoleRepository<TKey, TRole>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public RoleRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public TRole GetByRoleName(string roleName)
        {
            return DbSet.FirstOrDefault(a => a.Name.ToUpper() == roleName.ToUpper());
        }

        public List<TRole> GetRolesForUser(TKey userId)
        {
            //var query = from userRole in _userRoles
            //            where userRole.UserId.Equals(userId)
            //            join role in _roleStore.DbEntitySet on userRole.RoleId equals role.Id
            //            select role.Name;


            //foreach (var role in ctx.Roles)
            //{
            //    foreach (var user in role.Users.Where(a => a.Id == id))
            //    {
            //        Console.WriteLine(role.Name);
            //    }
            //}
            //return (DbSet.SelectMany(role => role.Users.Where(a => a.UserId == userId), (role, user) => role)).ToList();
            //return DbSet.All(a => a.Users.Where(b => b.UserId.Equals(userId))).ToList();
            return (from role in DbSet from user in role.Users.Where(a => a.UserId.Equals(userId)) select role).ToList();
        }
    }
}