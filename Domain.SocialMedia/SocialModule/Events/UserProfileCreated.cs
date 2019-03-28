using System;
using Swaksoft.Core;
using Swaksoft.Core.External;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Seedwork.Events;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Events
{
    public class UserProfileCreated : IDomainEvent
    {
        public ExternalProviderCredentials ClientCredentials { get; set; }
        public int UserProfileId { get; private set; }
        public OAuthToken AuthorizationToken { get; private set; }
        public string Message { get; private set; }

        public UserProfileCreated(
            ExternalProviderCredentials clientCredentials,
            int userProfileId,
            OAuthToken authorizationToken,
            string message)
        {
            if (clientCredentials == null) throw new ArgumentNullException("clientCredentials");
            if (authorizationToken == null) throw new ArgumentNullException("authorizationToken");
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException("message");

            ClientCredentials = clientCredentials;
            UserProfileId = userProfileId;
            AuthorizationToken = authorizationToken;
            Message = message;
        }
    }
}
