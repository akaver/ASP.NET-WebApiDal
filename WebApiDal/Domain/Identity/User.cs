using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Domain.Identity
{
    /// <summary>
    ///     IUser implementation, PK - int
    /// </summary>
    public class UserInt : User<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>
    {
        /// <summary>
        ///     Constructor which creates a new Guid for the Id
        /// </summary>
        public UserInt()
        {
            //nothing to do, PK is initialized by DB
        }

        /// <summary>
        ///     Constructor that takes a userName
        /// </summary>
        /// <param name="userName"></param>
        public UserInt(string userName)
            : this()
        {
            UserName = userName;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserInt, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserInt, int> manager, string authType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authType);
            // Add custom user claims here
            return userIdentity;
        }


        //Additional (custom) properties
        public virtual List<Person> Persons { get; set; }

    }


    /// <summary>
    ///     IUser implementation, PK - string
    /// </summary>
    public class User : User<string, Role, User, UserClaim, UserLogin, UserRole>, IUser
    {
        /// <summary>
        ///     Constructor which creates a new Guid for the Id
        /// </summary>
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Constructor that takes a userName
        /// </summary>
        /// <param name="userName"></param>
        public User(string userName)
            : this()
        {
            UserName = userName;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    /// <summary>
    ///     IUser implementation, generic
    /// </summary>
    public class User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : IUser<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public TKey Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "FirstLastName", ResourceType = typeof (Resources.Domain))]
        public string FirstLastName => FirstName + " " + LastName;

        [Display(Name = "LastFirstName", ResourceType = typeof (Resources.Domain))]
        public string LastFirstName => LastName + " " + FirstName;

        public virtual ICollection<TUserClaim> Claims { get; set; } = new List<TUserClaim>();
        public virtual ICollection<TUserLogin> Logins { get; set; } = new List<TUserLogin>();
        public virtual ICollection<TUserRole> Roles { get; set; } = new List<TUserRole>();
    }
}