using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;
using Interfaces.Repositories;
using Microsoft.Owin.Security;

namespace WebApi.DAL.Repositories
{
    public class UserRoleIntRepository :
        UserRoleRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserRoleIntRepository
    {
        public UserRoleIntRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
    }

    public class UserRoleRepository : UserRoleRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserRoleRepository
    {
        public UserRoleRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
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
        public UserRoleRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public TUserRole GetByUserIdAndRoleId(TKey roleId, TKey userId)
        {
            throw new NotImplementedException();
        }
    }
}
