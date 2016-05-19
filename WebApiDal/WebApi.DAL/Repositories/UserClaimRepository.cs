using System;
using System.Collections.Generic;
using System.Net.Http;
using Domain.Identity;
using Interfaces.Repository;

namespace WebApi.DAL.Repositories
{
    public class UserClaimIntRepository :
        UserClaimRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserClaimIntRepository
    {
        public UserClaimIntRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
    }

    public class UserClaimRepository : UserClaimRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserClaimRepository
    {
        public UserClaimRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
    }

    public class UserClaimRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : WebApiRepository<TUserClaim>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public UserClaimRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }

        public List<TUserClaim> AllIncludeUser()
        {
            throw new NotImplementedException();

        }

    }
}