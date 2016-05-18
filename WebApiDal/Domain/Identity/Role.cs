using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNet.Identity;

namespace Domain.Identity
{
    /// <summary>
    ///     Represents a Role entity, PK - int
    /// </summary>
    public class RoleInt : Role<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>
    {
        public RoleInt()
        {
        }

        public RoleInt(string roleName) : this()
        {
            Name = roleName;
        }
    }

    /// <summary>
    ///     Represents a Role entity, PK - string
    /// </summary>
    public class Role : Role<string, Role, User, UserClaim, UserLogin, UserRole>
    {
        /// <summary>
        ///     Constructor, initializes Id with new Guid
        /// </summary>
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="roleName">name of the role</param>
        public Role(string roleName)
            : this()
        {
            Name = roleName;
        }
    }

    public class Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : IRole<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public Role()
        {
            Users = new List<TUserRole>();
        }

        public TKey Id { get; set; }

        [DisplayName("Role name")]
        public string Name { get; set; }

        public virtual ICollection<TUserRole> Users { get; set; }
    }
}