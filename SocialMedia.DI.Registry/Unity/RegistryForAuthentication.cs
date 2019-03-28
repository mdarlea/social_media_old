using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Practices.Unity;
using Swaksoft.Infrastructure.Crosscutting.Authorization;
using Swaksoft.Infrastructure.Crosscutting.Authorization.EntityFramework;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.Entities;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.EntityFramework;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Token;

namespace SocialMedia.DI.Registry.Unity
{
    public class RegistryForAuthentication : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
           container.RegisterType<AspNetDbContext<ApplicationUser>,ApplicationUserDbContext>(
                new PerResolveLifetimeManager(),
                new InjectionConstructor("SocialMediaDataSource"));

            container.RegisterType<IIdentityMessageService, UserMessageService>(
                new PerResolveLifetimeManager());

            container.RegisterType<ApplicationSignInManager>(
              new PerResolveLifetimeManager());

            container.RegisterType<Swaksoft.Infrastructure.Crosscutting.Authorization.UserManager<ApplicationUser>, ApplicationUserManager>(
               new PerResolveLifetimeManager());
            container.RegisterType<Microsoft.AspNet.Identity.UserManager<ApplicationUser>>(
                new InjectionFactory(
                    o => o.Resolve<Swaksoft.Infrastructure.Crosscutting.Authorization.UserManager<ApplicationUser>>()));

            container.RegisterType<Swaksoft.Infrastructure.Crosscutting.Authorization.IUserStore<ApplicationUser>,AspNetUserStore<ApplicationUser>>();
            container.RegisterType<Microsoft.AspNet.Identity.IUserStore<ApplicationUser>>(
                new InjectionFactory(o => o.Resolve<Swaksoft.Infrastructure.Crosscutting.Authorization.IUserStore<ApplicationUser>>()));

            container.RegisterType<IAuthenticationTicketFactory<ApplicationUser>, AuthenticationTicketFactory<ApplicationUser>>();
            container.RegisterType<IAuthenticationTokenFactory, AuthenticationTokenFactory<ApplicationUser>>();
            container.RegisterType<IAccessTokenGenerator<ApplicationUser>, AccessTokenGenerator<ApplicationUser>>();
        }
    }}