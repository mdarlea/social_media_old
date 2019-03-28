using Microsoft.Practices.Unity;
using Swaksoft.Application.SocialMedia.SocialModule.Services;
using Swaksoft.Application.SocialMedia.SocialModule.Services.Providers;
using Swaksoft.Core.External;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Foursquare = Swaksoft.Infrastructure.AntiCorruption.Foursquare;

namespace SocialMedia.DI.Registry.Unity.Social
{
    public class RegistryForFoursquare : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            var foursquare = ExternalProvider.Foursquare.ToString();
            
            container.RegisterType<IOAuthAuthorizationAdapter, Foursquare.Services.OAuthAuthorizationAdapter>(foursquare);
            container.RegisterType<IUserProfileAdapter, Foursquare.Services.UserProfileAdapter>(foursquare);

            container.RegisterType<IProviderAppService, FoursquareProviderAppService>(
                foursquare,
                new InjectionConstructor(
                    new ResolvedParameter<IOAuthAuthorizationAdapter>(foursquare),
                    new ResolvedParameter<IUserProfileAdapter>(foursquare),
                    new ResolvedParameter<IUserRepository>(),
                    new ResolvedParameter<IUserProfileRepository>()));
            container.RegisterType<IOAuthAppService, OAuthAppService>(
                foursquare,
                new InjectionConstructor(new ResolvedParameter<IOAuthAuthorizationAdapter>(foursquare)));

           
        }
    }
}