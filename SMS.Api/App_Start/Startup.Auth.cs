using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SMS.Api.Providers;
using SMS.Api.Models;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SMS.Api
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

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
        }
    }
}
