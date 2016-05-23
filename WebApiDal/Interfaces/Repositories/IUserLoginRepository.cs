using System.Collections.Generic;
using Domain.Identity;

namespace Interfaces.Repositories
{
    public interface IUserLoginIntRepository : IUserLoginRepository<UserLoginInt>
    {
    }

    public interface IUserLoginRepository : IUserLoginRepository<UserLogin>
    {
    }

    public interface IUserLoginRepository<TUserLogin> : IBaseRepository<TUserLogin>
        where TUserLogin : class
    {
        List<TUserLogin> GetAllIncludeUser();
        TUserLogin GetUserLoginByProviderAndProviderKey(string loginProvider, string providerKey);
    }
}