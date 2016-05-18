using System;
using System.Web;
using DAL;
using DAL.Helpers;
using DAL.Interfaces;
using Domain.Identity;
using Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Web;
using Web.Helpers;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof (NinjectWebCommon), "Stop")]

namespace Web
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDbContext>().To<DataBaseContext>().InRequestScope();
            //kernel.Bind<IDbContextFactory>().To<DbContextFactory>().InRequestScope();

            kernel.Bind<EFRepositoryFactories>().To<EFRepositoryFactories>().InSingletonScope();
            kernel.Bind<IEFRepositoryProvider>().To<EFRepositoryProvider>().InRequestScope();
            kernel.Bind<IUOW>().To<UOW>().InRequestScope();

            kernel.Bind<IUserStore<User>>().To<UserStore>().InRequestScope();
            kernel.Bind<IRoleStore<Role>>().To<RoleStore>();
            kernel.Bind<IUserStore<UserInt, int>>().To<UserStoreInt>().InRequestScope();
            kernel.Bind<IRoleStore<RoleInt, int>>().To<RoleStoreInt>().InRequestScope();

            kernel.Bind<ApplicationSignInManager>().To<ApplicationSignInManager>().InRequestScope();
            kernel.Bind<ApplicationUserManager>().To<ApplicationUserManager>().InRequestScope();
            kernel.Bind<ApplicationRoleManager>().To<ApplicationRoleManager>().InRequestScope();

            kernel.Bind<IAuthenticationManager>().ToMethod(a => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();

            // http://stackoverflow.com/questions/5646820/logger-wrapper-best-practice
            kernel.Bind<NLog.ILogger>().ToMethod(a => NLog.LogManager.GetCurrentClassLogger());

            kernel.Bind<IUserNameResolver>().ToMethod(a => new UserNameResolver(UserNameFactory.GetCurrentUserNameFactory())).InSingletonScope();
        }
    }
}