using System;
using System.Collections.Generic;
using Domain.Identity;

namespace Interfaces.Repositories
{
    public interface IUserClaimIntRepository : IUserClaimRepository<int, UserClaimInt>
    {
    }

    public interface IUserClaimRepository : IUserClaimRepository<string, UserClaim>
    {
    }

    public interface IUserClaimRepository<TKey, TUserClaim> : IBaseRepository<TUserClaim>
        where TUserClaim : class
        where TKey: IEquatable<TKey>
    {
        List<TUserClaim> AllIncludeUser();
        List<TUserClaim> AllForUserId(TKey userId);
    }
}