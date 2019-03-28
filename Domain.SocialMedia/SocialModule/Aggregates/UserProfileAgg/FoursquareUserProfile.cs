using Swaksoft.Core;
using Swaksoft.Core.External;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg
{
    public class FoursquareUserProfile : UserProfile
    {
        public FoursquareUserProfile()
        {
            ProviderKey = ExternalProvider.Foursquare;
        }
    }
}
