using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;
using Domain.Identity;
using Microsoft.AspNet.Identity;
using NLog;

namespace Identity
{
    /// <summary>
    ///     EntityFramework based user store implementation that supports IUserStore, IUserLoginStore, IUserClaimStore and
    ///     IUserRoleStore. PK - int
    /// </summary>
    public class UserStoreInt : UserStore<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt,
        IRoleIntRepository, IUserClaimIntRepository, IUserLoginIntRepository, IUserIntRepository, IUserRoleIntRepository
        >
    {
        private readonly NLog.ILogger _logger;
        public UserStoreInt(IUOW uow, ILogger logger)
            : base(uow, logger)
        {
            _logger = logger;
        }
    }

    /// <summary>
    ///     EntityFramework based user store implementation that supports IUserStore, IUserLoginStore, IUserClaimStore and
    ///     IUserRoleStore. PK - string
    /// </summary>
    public class UserStore : UserStore<string, Role, User, UserClaim, UserLogin, UserRole,
        IRoleRepository, IUserClaimRepository, IUserLoginRepository, IUserRepository, IUserRoleRepository>,
        IUserStore<User>
    {
        private readonly NLog.ILogger _logger;
        public UserStore(IUOW uow, ILogger logger)
            : base(uow, logger)
        {
            _logger = logger;
        }
    }

    /// <summary>
    ///     EntityFramework based user store implementation that supports IUserStore, IUserLoginStore, IUserClaimStore and
    ///     IUserRoleStore
    /// </summary>
    public class UserStore
        <TKey,
            TRole, TUser, TUserClaim, TUserLogin, TUserRole,
            TRoleRepository, TUserClaimRepository, TUserLoginRepository, TUserRepository, TUserRoleRepository> :
                IUserLoginStore<TUser, TKey>,
                IUserClaimStore<TUser, TKey>,
                IUserRoleStore<TUser, TKey>,
                IUserPasswordStore<TUser, TKey>,
                IUserSecurityStampStore<TUser, TKey>,
                IUserStore<TUser, TKey>,
                IUserEmailStore<TUser, TKey>,
                IUserPhoneNumberStore<TUser, TKey>,
                IUserTwoFactorStore<TUser, TKey>,
                IUserLockoutStore<TUser, TKey>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>, new()
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>, new()
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>, new()
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>, new()
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>, new()
        where TRoleRepository : class, IRoleRepository<TKey, TRole>
        where TUserClaimRepository : class, IUserClaimRepository<TUserClaim>
        where TUserLoginRepository : class, IUserLoginRepository<TUserLogin>
        where TUserRepository : class, IUserRepository<TKey, TUser>
        where TUserRoleRepository : class, IUserRoleRepository<TKey, TUserRole>

    {
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;
        private bool _disposed;

        public UserStore(IUOW uow, ILogger logger)
        {
            _uow = uow;
            _logger = logger;

            _logger.Debug("InstanceId: " + _instanceId);
        }

        #region dispose

        public void Dispose()
        {
            _logger.Debug("InstanceId: " + _instanceId);
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        #endregion

        #region IUserStore

        public Task CreateAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _uow.GetRepository<TUserRepository>().Add(user);
            _uow.Commit();
            return Task.FromResult<Object>(null);
        }

        public Task UpdateAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _uow.GetRepository<TUserRepository>().Update(user);
            _uow.Commit();

            return Task.FromResult<Object>(null);
        }

        public Task DeleteAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _uow.GetRepository<TUserRepository>().Delete(user);
            _uow.Commit();

            return Task.FromResult<Object>(null);
        }

        public Task<TUser> FindByIdAsync(TKey userId)
        {
            _logger.Info("_instanceId: " + _instanceId + " userId:" + userId);
            ThrowIfDisposed();
            var res = _uow.GetRepository<TUserRepository>().GetById(userId);
            _logger.Info("result: " + (res == null ? "null" : res.Email));
            return Task.FromResult(res);
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            return Task.FromResult(_uow.GetRepository<TUserRepository>().GetUserByUserName(userName));
        }

        #endregion IUserStore

        #region IUserPasswordStore

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PasswordHash = passwordHash;

            _logger.Info("User.Id: " + user.Id + " PasswordHash: " + passwordHash);

            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            return Task.FromResult(user.PasswordHash != null);
        }

        #endregion IUserPasswordStore

        #region IUserEmailStore

        public Task SetEmailAsync(TUser user, string email)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<TUser> FindByEmailAsync(string email)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            return Task.FromResult<TUser>(_uow.GetRepository<TUserRepository>().GetUserByEmail(email));
        }

        #endregion

        #region IUserLockoutStore

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return
                Task.FromResult(user.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? (DateTime?) null : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        #endregion IUserLockoutStore

        #region IUserTwoFactorStore

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.TwoFactorEnabled);
        }

        #endregion

        #region IUserPhoneNumberStore

        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        #endregion

        #region IUserLoginStore

        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            _uow.GetRepository<TUserLoginRepository>().Add(new TUserLogin
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider
            });
            return Task.FromResult(0);
        }

        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var userLogin =
                _uow.GetRepository<TUserLoginRepository>()
                    .GetUserLoginByProviderAndProviderKey(login.LoginProvider, login.ProviderKey);

            if (userLogin == null || userLogin.UserId.Equals(user.Id)) return Task.FromResult(0);

            _uow.GetRepository<TUserLoginRepository>().Delete(userLogin);
            _uow.Commit();

            return Task.FromResult(0);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return
                Task.FromResult(
                    user.Logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList() as
                        IList<UserLoginInfo>);
        }

        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            _logger.Info("_instanceId: " + _instanceId + " login.LoginProvider: " + login.LoginProvider +
                         " login.ProviderKey: " + login.ProviderKey);

            ThrowIfDisposed();
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var userLogin =
                _uow.GetRepository<TUserLoginRepository>()
                    .GetUserLoginByProviderAndProviderKey(login.LoginProvider, login.ProviderKey);
            if (userLogin != null)
            {
                _logger.Info("found userLogin.LoginProvider: " + userLogin.LoginProvider);
                var user = _uow.GetRepository<TUserRepository>().GetById(userLogin.UserId);
                if (user != null)
                {
                    _logger.Info("found user:" + user.Email);
                }
                else
                {
                    _logger.Error("user not found!!!");
                }

                return Task.FromResult(user);
            }
            return Task.FromResult<TUser>(null);
        }

        #endregion IUserLoginStore

        #region IUserRoleStore

        public Task AddToRoleAsync(TUser user, string roleName)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("roleName");
            }

            var role = _uow.GetRepository<TRoleRepository>().GetByRoleName(roleName);
            if (role == null)
            {
                throw new InvalidOperationException(roleName);
            }

            _uow.GetRepository<TUserRoleRepository>().Add(new TUserRole() {UserId = user.Id, RoleId = role.Id});
            _uow.Commit();

            return Task.FromResult(0);
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            _logger.Info("_instanceId: " + _instanceId + " user:" + user.Email + " roleName:" + roleName);

            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException("roleName");
            }

            var role = _uow.GetRepository<TRoleRepository>().GetByRoleName(roleName);

            if (role == null) return Task.FromResult(0);

            var userRole = _uow.GetRepository<TUserRoleRepository>().GetByUserIdAndRoleId(role.Id, user.Id);

            if (userRole == null) return Task.FromResult(0);

            _uow.GetRepository<TUserRoleRepository>().Delete(userRole);
            _uow.Commit();

            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            _logger.Info("_instanceId: " + _instanceId + " user.Email:" + user.Email);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var roles = _uow.GetRepository<TRoleRepository>().GetRolesForUser(user.Id);
            var roleNameList = roles.Select(a => a.Name).ToList();

            return Task.FromResult<IList<string>>(roleNameList);
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            _logger.Info("_instanceId: " + _instanceId + " roleName:" + roleName);
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("roleName");
            }

            return Task.FromResult(_uow.GetRepository<TUserRepository>().IsInRole(user.Id, roleName));
        }

        #endregion

        #region IUserClaimStore

        public Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(TUser user)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return
                Task.FromResult<IList<System.Security.Claims.Claim>>(
                    user.Claims.Select(c => new System.Security.Claims.Claim(c.ClaimType, c.ClaimValue)).ToList());
        }

        public Task AddClaimAsync(TUser user, System.Security.Claims.Claim claim)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("user");
            }

            _logger.Info("claim.value: " + claim.Value + " claim.type: " + claim.Type);

            _uow.GetRepository<TUserClaimRepository>()
                .Add(new TUserClaim() {ClaimValue = claim.Value, ClaimType = claim.Type, UserId = user.Id});
            _uow.Commit();
            return Task.FromResult<object>(null);
        }

        public Task RemoveClaimAsync(TUser user, System.Security.Claims.Claim claim)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            var c =
                _uow.GetRepository<TUserClaimRepository>()
                    .All.FirstOrDefault(
                        a => a.ClaimType == claim.Type && a.ClaimValue == claim.Value && a.UserId.Equals(user.Id));
            if (c != null)
            {
                _uow.GetRepository<TUserClaimRepository>().Delete(c);
                _uow.Commit();
            }

            return Task.FromResult<object>(null);
        }

        #endregion

        #region IUserSecurityStampStore

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            _logger.Info("_instanceId: " + _instanceId + " stamp:" + stamp);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(TUser user)
        {
            _logger.Info("_instanceId: " + _instanceId + " user.SecurityStamp:" + user.SecurityStamp);

            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.SecurityStamp);
        }

        #endregion IUserSecurityStampStore
    }
}