using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace DAL.Interfaces
{
    public interface IUserLoginIntRepository : IUserLoginRepository<UserLoginInt>
    {
    }

    public interface IUserLoginRepository : IUserLoginRepository<UserLogin>
    {
    }

    public interface IUserLoginRepository<TUserLogin> : IEFRepository<TUserLogin>
        where TUserLogin : class
    {
        List<TUserLogin> GetAllIncludeUser();
        TUserLogin GetUserLoginByProviderAndProviderKey(string loginProvider, string providerKey);
    }
}