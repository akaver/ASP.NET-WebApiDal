namespace Domain.Identity
{
    /// <summary>
    ///     EntityType that represents one specific user claim, PK - int
    /// </summary>
    public class UserClaimInt : UserClaim<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>
    {
    }


    /// <summary>
    ///     EntityType that represents one specific user claim, PK - string
    /// </summary>
    public class UserClaim : UserClaim<string, Role, User, UserClaim, UserLogin, UserRole>
    {
    }

    /// <summary>
    ///     EntityType that represents one specific user claim, generic
    ///     TKey - type for Id in other classes (string, int)
    ///     UserClaim is using int as PK
    /// </summary>
    public class UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public int Id { get; set; }

        public TKey UserId { get; set; }
        public virtual TUser User { get; set; }

        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}