using System;
using System.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.ServiceLocation;
using Owin;
using SocialMedia.Host.Authentication;
using SocialMedia.Host.Authentication.Providers;
using Swaksoft.Core.External;

namespace SocialMedia.Host
{
    public partial class Startup
    {
        internal static TimeSpan GetAccessTokenExpireTimeSpan()
        {
            var setting = ConfigurationManager.AppSettings["AccessTokenExpireTimeSpan"];
            int days;
            int.TryParse(setting, out days);
            if (days < 1)
            {
                days = 1;
            }
            return TimeSpan.FromDays(days);
        }

        internal static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }        

        private static readonly object thisObject = new object();

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            
            //app.CreatePerOwinContext(ApplicationUserDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            var provider = ServiceLocator.Current.GetInstance<OAuthAuthorizationServerProvider>();
            var authenticationTokenProvider = ServiceLocator.Current.GetInstance<IAuthenticationTokenProvider>();

            var googleProvider = ServiceLocator.Current.GetInstance<IGoogleOAuth2AuthenticationProvider>();
            var facebookProvider = ServiceLocator.Current.GetInstance<FacebookAuthenticationProvider>();
            //var twitterProvider = DependencyResolver.Current.GetService<TwitterAuthenticationProvider>();

            var oauthOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = GetAccessTokenExpireTimeSpan(),
                Provider = provider,
                AccessTokenProvider = authenticationTokenProvider
            };

            lock (thisObject)
            {
                OAuthBearerOptions = new OAuthBearerAuthenticationOptions
                {
                    AccessTokenProvider = authenticationTokenProvider,
                    Provider = new QueryStringEnabledOAuthBearerAuthenticationProvider()
                };
           }

            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // Token Generation
            //app.UseOAuthBearerTokens(oauthOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
            app.UseOAuthAuthorizationServer(oauthOptions);
            

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            ////get the configuration settings for external providers
            var configSection = Swaksoft.Configuration.Social.ConfigurationSettings.Current;

            //var twitter = configSection.ProvidersCollection[ExternalProvider.Twitter];
            var facebook = configSection.ProvidersCollection[ExternalProvider.Facebook];
            var google = configSection.ProvidersCollection[ExternalProvider.Google];

            //Configure Facebook External Login
            var facebookAuthOptions = new FacebookAuthenticationOptions
            {
                AppId = facebook.ConsumerKey,
                AppSecret = facebook.ConsumerSecret,
                Provider = facebookProvider                
            };

            //    TwitterAuthOptions = new TwitterAuthenticationOptions
            //    {
            //        ConsumerKey = twitter.ConsumerKey,
            //        ConsumerSecret = twitter.ConsumerSecret,
            //        Provider = twitterProvider,
            //        BackchannelCertificateValidator = new Microsoft.Owin.Security.CertificateSubjectKeyIdentifierValidator(new[]
            //        {
            //            "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
            //            "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
            //            "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
            //            "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
            //            "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5
            //            "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server C‎A 
            //            "B13EC36903F8BF4701D498261A0802EF63642BC3" // DigiCert High Assurance EV Root CA
            //        })
            //    };

            var googleAuthOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = google.ConsumerKey,
                ClientSecret = google.ConsumerSecret,
                Provider = googleProvider
            };
            //}

            app.UseFacebookAuthentication(facebookAuthOptions);
            //app.UseTwitterAuthentication(TwitterAuthOptions);
            app.UseGoogleAuthentication(googleAuthOptions);
        }
    }
}
