using System;
using Swaksoft.Core;
using Swaksoft.Core.External;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts
{
    public class OAuthOptions
    {
        public ExternalProviderCredentials ClientCredentials { get; set; }

        public string OAuthToken { get; set; }

        public string OAuthVerifier { get; set; }

        public Uri CallbackUrl { get; set; }
    }
}
