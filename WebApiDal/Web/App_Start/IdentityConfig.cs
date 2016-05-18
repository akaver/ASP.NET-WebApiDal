using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using NLog;
using Web.Models;

namespace Web
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<UserInt, int>
    {
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();

        public ApplicationUserManager(IUserStore<UserInt, int> store, ILogger logger)
            : base(store)
        {
            _logger = logger;
            _logger.Debug("InstanceId: " + _instanceId);

            // Configure validation logic for usernames
            UserValidator = new UserValidator<UserInt, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<UserInt, int>
            {
                MessageFormat = "Your security code is {0}"
            });
            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<UserInt, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            //EmailService = new EmailService();
            //SmsService = new SmsService();
            if (Startup.DataProtectionProvider != null)
            {
                UserTokenProvider =
                    new DataProtectorTokenProvider<UserInt, int>(
                        Startup.DataProtectionProvider.Create("ASP.NET Identity"));
            }
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<UserInt, int>
    {
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager, ILogger logger)
            : base(userManager, authenticationManager)
        {
            _logger = logger;
            _logger.Debug("InstanceId: " + _instanceId);
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(UserInt user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager) UserManager);
        }

        //public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        //{
        //	return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        //}
    }

    public class ApplicationRoleManager : RoleManager<RoleInt, int>
    {
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();

        public ApplicationRoleManager(IRoleStore<RoleInt, int> store, ILogger logger) : base(store)
        {
            _logger = logger;
            _logger.Debug("InstanceId: " + _instanceId);
            RoleValidator = new RoleValidator<RoleInt, int>(this);
        }
    }


    /*
    // API project generated stuff
        public class ApplicationUserManager : UserManager<ApplicationUser>
        {
            public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
            {
            }

            public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
                // Configure validation logic for usernames
                manager.UserValidator = new UserValidator<ApplicationUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };
                // Configure validation logic for passwords
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                };
                var dataProtectionProvider = options.DataProtectionProvider;
                if (dataProtectionProvider != null)
                {
                    manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
                }
                return manager;
            }
        }
    */
}