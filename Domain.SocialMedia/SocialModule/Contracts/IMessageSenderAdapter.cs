using System;
using Swaksoft.Core;
using Swaksoft.Core.External;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts
{
    public interface IMessageSenderAdapter
    {
        TweetResult SendMessage(
            ExternalProviderCredentials clientCredentials,
            int userProfileId,
            OAuthToken authorizationToken,
            TweetOptions options);
    }
}
