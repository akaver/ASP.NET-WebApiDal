using System;
using System.Net.Http;
using Domain.Identity;
using Interfaces.Repository;
using NLog;

namespace WebApi.DAL.Repositories
{
    public class UserIntRepository : UserRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>,
        IUserIntRepository
    {
        public UserIntRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
    }

    public class UserRepository : UserRepository<string, Role, User, UserClaim, UserLogin, UserRole>, IUserRepository
    {
        public UserRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
    }

    public class UserRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : WebApiRepository<TUser>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public UserRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
        public TUser GetUserByUserName(string userName)
        {
            //return DbSet.FirstOrDefault(a => a.UserName.ToUpper() == userName.ToUpper());

            var response = HttpClient.GetAsync(EndPoint + nameof(GetUserByUserName) + "/" + userName+"/").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<TUser>().Result;
                return res;
            }
            _logger.Debug("Web API statuscode: " + response.StatusCode.ToString() + " Uri:" + response.RequestMessage.RequestUri);
            return null;
        }

        public TUser GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool IsInRole(TKey userId, string roleName)
        {
            throw new NotImplementedException();
        }

        public void AddUserToRole(TKey userId, string roleName)
        {
            throw new NotImplementedException();
        }
    }
}