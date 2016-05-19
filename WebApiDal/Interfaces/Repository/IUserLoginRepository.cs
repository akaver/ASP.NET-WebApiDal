using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Interfaces.Repository
{
    public interface IUserLoginIntRepository : IUserLoginRepository<UserLoginInt>
    {
    }

    public interface IUserLoginRepository : IUserLoginRepository<UserLogin>
    {
    }

    public interface IUserLoginRepository<TUserLogin> : IRepository<TUserLogin>
        where TUserLogin : class
    {
        List<TUserLogin> GetAllIncludeUser();
        TUserLogin GetUserLoginByProviderAndProviderKey(string loginProvider, string providerKey);
    }
}