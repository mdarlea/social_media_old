using Swaksoft.Core;
using Swaksoft.Core.External;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class ExternalProfileRequest
    {
        public string UserId { get; set; }

        public string CallbackUrl { get; set; }

        public ExternalProviderCredentials ClientCredentials { get; set; }

        public string AccessToken { get; set; }

        public string AccessTokenSecret { get; set; }

        public string SubscriptionMessage { get; set; }
    }
}
