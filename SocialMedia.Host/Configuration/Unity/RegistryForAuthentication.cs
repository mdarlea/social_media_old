using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Practices.Unity;
using SocialMedia.DI.Registry;
using SocialMedia.Host.Authentication.Providers;

namespace SocialMedia.Host.Configuration.Unity
{
    public class RegistryForAuthentication : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterType<IAuthenticationManager>(
               new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<IDataProtectionProvider>(
                new InjectionFactory(o => Startup.DataProtectionProvider));

            container.RegisterType<ISecureDataFormat<AuthenticationTicket>>(
                           new InjectionFactory(o => Startup.OAuthBearerOptions.AccessTokenFormat));

            container.RegisterType<IGoogleOAuth2AuthenticationProvider, GoogleAuthProvider>();
            container.RegisterType<FacebookAuthenticationProvider, FacebookAuthProvider>();
            
            container.RegisterType<IAuthenticationTokenProvider, Authentication.Providers.AuthenticationTokenProvider>(
                new ContainerControlledLifetimeManager());

        }
    }
}