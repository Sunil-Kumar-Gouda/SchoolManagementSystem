﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using SMS.Web.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace SMS.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Dashboard/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser,int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) =>
                        user.GenerateUserIdentityAsync(manager),
                        getUserIdCallback: (id) => (id.GetUserId<int>()))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

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

            var googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "3585416697-ratm1lh8646cnri4edfnqm4smau12bo0.apps.googleusercontent.com",
                ClientSecret = "Feq-8w_XteQ0pfjShD5p4v46",
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("Google_AccessToken", context.AccessToken));

                        if (context.RefreshToken != null)
                        {
                            context.Identity.AddClaim(new Claim("GoogleRefreshToken", context.RefreshToken));
                        }
                        context.Identity.AddClaim(new Claim("GoogleUserId", context.Id));
                        context.Identity.AddClaim(new Claim("GoogleTokenIssuedAt", DateTime.Now.ToBinary().ToString()));
                        var expiresInSec = (long)(context.ExpiresIn.Value.TotalSeconds);
                        context.Identity.AddClaim(new Claim("GoogleTokenExpiresIn", expiresInSec.ToString()));


                        return Task.FromResult(0);
                    }
                }
            };
            googleAuthOptions.Scope.Add("email");
            googleAuthOptions.Scope.Add("openid");
            googleAuthOptions.Scope.Add("profile");
            googleAuthOptions.Scope.Add("email");
            app.UseGoogleAuthentication(googleAuthOptions);
            createRolesandUsers();
        }
        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            
            var roleManager = new RoleManager<CustomRole,int>(new CustomRoleStore(context));
            var UserManager = new UserManager<ApplicationUser,int>(new CustomUserStore(context));

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin rool   
                var role = new CustomRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website
                var user = new ApplicationUser();
                user.UserName = ConfigurationManager.AppSettings["AdminUserName"];

                user.Email = ConfigurationManager.AppSettings["AdminEmailId"];
                var x = ConfigurationManager.GetSection("appSettings");
                string userPWD = ConfigurationManager.AppSettings["AdminPassword"];

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Teacher role    
            if (!roleManager.RoleExists("Teacher"))
            {
                var role = new CustomRole();
                role.Name = "Teacher";
                roleManager.Create(role);
            }

            // creating Creating Student role    
            if (!roleManager.RoleExists("Student"))
            {
                var role = new CustomRole();
                role.Name = "Student";
                roleManager.Create(role);
            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Staff"))
            {
                var role = new CustomRole();
                role.Name = "Staff";
                roleManager.Create(role);
            }
        }
    }
}