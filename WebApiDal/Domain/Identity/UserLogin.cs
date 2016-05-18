namespace Domain.Identity
{
    /// <summary>
    ///     Entity type for a user's login (i.e. facebook, google), PK - int
    /// </summary>
    public class UserLoginInt : UserLogin<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>
    {
    }

    /// <summary>
    ///     Entity type for a user's login (i.e. facebook, google), PK - string
    /// </summary>
    public class UserLogin : UserLogin<string, Role, User, UserClaim, UserLogin, UserRole>
    {
    }

    /// <summary>
    ///     Entity type for a user's login (i.e. facebook, google), PK - string
    ///     TKey - type for PK (string, int)
    /// </summary>
    public class UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }

        public TKey UserId { get; set; }
        public virtual TUser User { get; set; }
    }
}