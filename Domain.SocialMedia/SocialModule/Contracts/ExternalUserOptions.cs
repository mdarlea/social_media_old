using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts
{
    public class ExternalUserOptions
    {
        public OAuthToken AuthorizationToken { get; set; }

        public int UserProfileId { get; set; }

        public string UserName { get; set; }
    }
}
