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
    public class UserLoginIntRepository :
     UserLoginRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserLoginIntRepository
    {
        public UserLoginIntRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
    }

    public class UserLoginRepository : UserLoginRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserLoginRepository
    {
        public UserLoginRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
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
        public UserLoginRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public List<TUserLogin> GetAllIncludeUser()
        {
            //return DbSet.Include(a => a.User).ToList();
            throw new NotImplementedException();
        }

        public TUserLogin GetUserLoginByProviderAndProviderKey(string loginProvider, string providerKey)
        {
            //return DbSet.FirstOrDefault(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);
            throw new NotImplementedException();
        }

    }
}
