using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;
using Interfaces.Repositories;
using Microsoft.Owin.Security;
using NLog;

namespace WebApi.DAL.Repositories
{
    public class RoleIntRepository : RoleRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>,
    IRoleIntRepository
    {
        public RoleIntRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
    }

    public class RoleRepository : RoleRepository<string, Role, User, UserClaim, UserLogin, UserRole>, IRoleRepository
    {
        public RoleRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
    }

    public class RoleRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : WebApiRepository<TRole>,
        IRoleRepository<TKey, TRole>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public RoleRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public TRole GetByRoleName(string roleName)
        {
            //return DbSet.FirstOrDefault(a => a.Name.ToUpper() == roleName.ToUpper());
            throw new NotImplementedException();
        }

        public List<TRole> GetRolesForUser(TKey userId)
        {
            // return (from role in DbSet from user in role.Users.Where(a => a.UserId.Equals(userId)) select role).ToList();

            var response = HttpClient.GetAsync(EndPoint + nameof(GetRolesForUser) + "/" + userId).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<TRole>>().Result;
                return res;
            }
            _logger.Debug("Web API statuscode: " + response.StatusCode.ToString() + " Uri:" + response.RequestMessage.RequestUri);
            return new List<TRole>();
        }

    }



}
