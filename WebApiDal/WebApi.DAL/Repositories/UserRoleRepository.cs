using System;
using System.Net.Http;
using Domain.Identity;
using Interfaces.Repository;

namespace WebApi.DAL.Repositories
{
    public class UserRoleIntRepository :
        UserRoleRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserRoleIntRepository
    {
        public UserRoleIntRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
    }

    public class UserRoleRepository : UserRoleRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserRoleRepository
    {
        public UserRoleRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
    }

    public class UserRoleRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : WebApiRepository<TUserRole>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public UserRoleRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }

        public TUserRole GetByUserIdAndRoleId(TKey roleId, TKey userId)
        {
            throw new NotImplementedException();
        }
    }
}