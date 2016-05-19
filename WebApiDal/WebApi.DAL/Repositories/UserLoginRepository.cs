using System;
using System.Collections.Generic;
using System.Net.Http;
using Domain.Identity;
using Interfaces.Repository;

namespace WebApi.DAL.Repositories
{
    public class UserLoginIntRepository :
        UserLoginRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserLoginIntRepository
    {
        public UserLoginIntRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
    }

    public class UserLoginRepository : UserLoginRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserLoginRepository
    {
        public UserLoginRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
    }

    public class UserLoginRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : WebApiRepository<TUserLogin>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public UserLoginRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }

        public List<TUserLogin> GetAllIncludeUser()
        {
            throw new NotImplementedException();
        }

        public TUserLogin GetUserLoginByProviderAndProviderKey(string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

    }
}