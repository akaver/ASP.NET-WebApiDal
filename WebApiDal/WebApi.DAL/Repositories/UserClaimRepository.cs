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


    public class UserClaimIntRepository :
    UserClaimRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserClaimIntRepository
    {
        public UserClaimIntRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
    }

    public class UserClaimRepository : UserClaimRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserClaimRepository
    {
        public UserClaimRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
    }

    public class UserClaimRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : WebApiRepository<TUserClaim>, IUserClaimRepository<TKey, TUserClaim>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        public UserClaimRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public List<TUserClaim> AllIncludeUser()
        {
            //return DbSet.Include(a => a.User).ToList();
            throw new NotImplementedException();
        }

        public List<TUserClaim> AllForUserId(TKey userId)
        {
            // return DbSet.Where(c => c.UserId.Equals(userId)).ToList();
            var response = HttpClient.GetAsync(EndPoint + nameof(AllForUserId)+"/"+userId).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<TUserClaim>>().Result;
                return res;
            }

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new List<TUserClaim>(); 
        }
    }
}
