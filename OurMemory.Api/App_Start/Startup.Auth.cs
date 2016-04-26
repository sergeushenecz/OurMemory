using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using OurMemory.Data;
using OurMemory.Providers;
using Owin;

namespace OurMemory
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            //            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";




            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true,

            };

            // Enable the application to use bearer tokens to authenticate users
//            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseOAuthAuthorizationServer(OAuthOptions);

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions()
            {
                Provider = new QueryStringOAuthBearerProvider(),
                AccessTokenProvider = new AuthenticationTokenProvider()
                {
                    OnCreate = create,
                    OnReceive = receive
                },
            };

            app.UseOAuthBearerAuthentication(OAuthBearerOptions);


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

        public static Action<AuthenticationTokenCreateContext> create = new Action<AuthenticationTokenCreateContext>(c =>
        {
            c.SetToken(c.SerializeTicket());
        });

        public static Action<AuthenticationTokenReceiveContext> receive = new Action<AuthenticationTokenReceiveContext>(c =>
        {
            c.DeserializeTicket(c.Token);
            c.OwinContext.Environment["Properties"] = c.Ticket.Properties;
        });
    }
}
