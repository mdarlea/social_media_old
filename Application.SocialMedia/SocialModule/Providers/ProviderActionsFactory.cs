using Swaksoft.Core;
using Swaksoft.Core.External;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;

namespace Swaksoft.Application.SocialMedia.SocialModule.Providers
{
    public class ProviderActionsFactory : ActionsFactory<IProviderFactory>
    {
        public ProviderActionsFactory()
        {
            Register(ExternalProvider.Twitter.ToString(), () => new ProviderFactory<TwitterUserProfile>());
            Register(ExternalProvider.Foursquare.ToString(), () => new ProviderFactory<FoursquareUserProfile>());
        }
    }
}
