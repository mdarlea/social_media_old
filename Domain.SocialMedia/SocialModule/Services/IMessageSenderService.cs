using System;
using Swaksoft.Core;
using Swaksoft.Core.External;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Services
{
    public interface IMessageSenderService
    {
        TweetResult SendMessage(
            ExternalProviderCredentials clientCredentials,
            int userProfileId,
            OAuthToken authorizationToken,
            TweetOptions options);
    }
}
