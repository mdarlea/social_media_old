using System;
using System.Collections.Generic;
using System.Linq;
using Swaksoft.Core;
using Swaksoft.Core.External;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Seedwork.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Swaksoft.Domain.SocialMedia.SocialModule.Events;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Services
{
    public class MessageSenderService : IMessageSenderService
    {
        private readonly IMessageSenderAdapter _messageSenderAdapter;

        public MessageSenderService(IMessageSenderAdapter messageSenderAdapter)
        {
            if (messageSenderAdapter == null) throw new ArgumentNullException("messageSenderAdapter");
            _messageSenderAdapter = messageSenderAdapter;
        }

        public TweetResult SendMessage(
            ExternalProviderCredentials clientCredentials, 
            int userProfileId, 
            OAuthToken authorizationToken,
            TweetOptions options)
        {
            if (clientCredentials == null) throw new ArgumentNullException("clientCredentials");
            if (authorizationToken == null) throw new ArgumentNullException("authorizationToken");
            if (options == null) throw new ArgumentNullException("options");

            var result = _messageSenderAdapter.SendMessage(clientCredentials, userProfileId, authorizationToken, options);

            var @event = new MessageSent(
                result.Id,
                result.UserId,
                result.UserName,
                result.InReplyToUserId,
                result.InReplyToUserName,
                result.CreatedDate)
            {
                UserProfileId = userProfileId,
                Text = options.Message,
                StreamedTweetId = options.StreamedTweetId
            };

            //raise message sent event
            DomainEvents.Raise(@event);

            return result;
        }
    }
}
