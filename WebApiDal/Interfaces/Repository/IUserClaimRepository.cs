using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Interfaces.Repository
{
    public interface IUserClaimIntRepository : IUserClaimRepository<UserClaimInt>
    {
    }

    public interface IUserClaimRepository : IUserClaimRepository<UserClaim>
    {
    }

    public interface IUserClaimRepository<TUserClaim> : IRepository<TUserClaim>
        where TUserClaim : class
    {
        List<TUserClaim> AllIncludeUser();
    }
}