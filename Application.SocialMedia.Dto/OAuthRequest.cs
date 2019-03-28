using System;
using Swaksoft.Core;
using Swaksoft.Core.External;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class OAuthRequest
    {
        public string UserId { get; set; }

        public Uri CallbackUrl { get; set; }

        public ExternalProviderCredentials ClientCredentials { get; set; }

        public string OAuthToken { get; set; }

        public string OAuthVerifier { get; set; }

        public string SubscriptionMessage { get; set; }
    }
}
