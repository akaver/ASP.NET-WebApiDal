using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Web.Providers;
using Web.Models;

namespace Web
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();

            // Configure the db context, user manager and signin manager to use a single instance per request
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // get the DI container (ninject kernel) through DependencyResolver, and get user manager through it
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager>());

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity =
                        SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, UserInt, int>(
                            validateInterval: TimeSpan.FromMinutes(5),
                            regenerateIdentityCallback: (manager, user) =>
                                user.GenerateUserIdentityAsync(manager),
                            // delete old cookies from browser first, otherwise youll get string conversion error
                            getUserIdCallback: (id) => id.GetUserId<int>())
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);




            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = false
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);


            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "1013371769079-e92itoloehi1rq4jd0ld0qgpssvd52t3.apps.googleusercontent.com",
            //    ClientSecret = "dK20iWqZ4wupyoAUyCcZKbli"
            //});
        }

        /*
                // API project code
                // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
                public void ConfigureAuth(IAppBuilder app)
                {
                    // Configure the db context and user manager to use a single instance per request
                    app.CreatePerOwinContext(ApplicationDbContext.Create);
                    app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

                    // Enable the application to use a cookie to store information for the signed in user
                    // and to use a cookie to temporarily store information about a user logging in with a third party login provider
                    app.UseCookieAuthentication(new CookieAuthenticationOptions());
                    app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

                    // Configure the application for OAuth based flow
                    PublicClientId = "self";
                    OAuthOptions = new OAuthAuthorizationServerOptions
                    {
                        TokenEndpointPath = new PathString("/Token"),
                        Provider = new ApplicationOAuthProvider(PublicClientId),
                        AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                        AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                        // In production mode set AllowInsecureHttp = false
                        AllowInsecureHttp = true
                    };

                    // Enable the application to use bearer tokens to authenticate users
                    app.UseOAuthBearerTokens(OAuthOptions);

                    // Uncomment the following lines to enable logging in with third party login providers
                    //app.UseMicrosoftAccountAuthentication(
                    //    clientId: "",
                    //    clientSecret: "");

                    //app.UseTwitterAuthentication(
                    //    consumerKey: "",
                    //    consumerSecret: "");

                    //app.UseFacebookAuthentication(
                    //    appId: "",
                    //    appSecret: "");

                    //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
                    //{
                    //    ClientId = "",
                    //    ClientSecret = ""
                    //});
                }
        */
    }
}