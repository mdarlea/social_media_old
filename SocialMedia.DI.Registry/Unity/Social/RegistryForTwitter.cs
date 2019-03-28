using Microsoft.Practices.Unity;
using Swaksoft.Application.SocialMedia.SocialModule.Services;
using Swaksoft.Application.SocialMedia.SocialModule.Services.Providers;
using Swaksoft.Core.External;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;

namespace SocialMedia.DI.Registry.Unity.Social
{
    public class RegistryForTwitter : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            var twitter = ExternalProvider.Twitter.ToString();
            
            //registers adapters
            container.RegisterType<IOAuthAuthorizationAdapter, Swaksoft.Infrastructure.AntiCorruption.Twitter.Services.OAuthAuthorizationAdapter>(twitter);
            container.RegisterType<IUserProfileAdapter, Swaksoft.Infrastructure.AntiCorruption.Twitter.Services.UserProfileAdapter>(twitter);
            container.RegisterType<IStreamingAdapter, Swaksoft.Infrastructure.AntiCorruption.Twitter.Services.StreamingAdapter>(twitter);

            container.RegisterType<IProviderAppService, TwitterProviderAppService>(
                twitter,
                new InjectionConstructor(
                    new ResolvedParameter<IOAuthAuthorizationAdapter>(twitter),
                    new ResolvedParameter<IUserProfileAdapter>(twitter),
                    new ResolvedParameter<IUserRepository>(),
                    new ResolvedParameter<IUserProfileRepository>()));
            container.RegisterType<IOAuthAppService, OAuthAppService>(
                twitter,
                new InjectionConstructor(new ResolvedParameter<IOAuthAuthorizationAdapter>(twitter)));
            container.RegisterType<IRealTimeStreamingAppService, TwitterStreamingAppService>(
                twitter,
                new InjectionConstructor(
                    new ResolvedParameter<IRealTimeStreamingFactory>(),
                    new ResolvedParameter<IStreamingAdapter>(twitter)));
        }
    }
}