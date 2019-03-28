using System;
using System.Linq;
using Swaksoft.Core;
using Swaksoft.Core.External;
using Swaksoft.Domain.Seedwork.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Swaksoft.Domain.SocialMedia.SocialModule.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Events.Streaming;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Services
{
    public class TweetProcessorService : ITweetProcessorService
    {
        private readonly IMessageSenderService _messageSenderService;

        public TweetProcessorService(IMessageSenderService messageSenderService)
        {
            if (messageSenderService == null) throw new ArgumentNullException("messageSenderService");
            _messageSenderService = messageSenderService;
        }

        public void ProcessStreamFilter(
            TwitterUserProfile userProfile, 
            StreamFilter streamFilter,
            ExternalProviderCredentials clientCredentials,
            StreamedTweet streamedTweet,
            Location location,
            DateTime dateSent)
        {
            if (userProfile == null) throw new ArgumentNullException("userProfile");
            if (streamFilter == null) throw new ArgumentNullException("streamFilter");
            if (streamedTweet == null) throw new ArgumentNullException("streamedTweet");
          
            //check if user is blocked
            var sentByUserName = streamedTweet.UserName;
            if (userProfile.IsUserBlocked(sentByUserName))
            {
                return;
            }

            var user = sentByUserName.ToLower();

            var messages = streamFilter.GetMessages().ToList();

            foreach (var message in
                        from replyMessage in messages
                        where !user.Equals(userProfile.UserName.ToLower())
                        select string.Format("@{0} {1}", sentByUserName, replyMessage)
                            into message
                            where !userProfile.MessageWasSent(sentByUserName, message)
                            select message)
            {
                _messageSenderService.SendMessage(
                    clientCredentials,
                    userProfile.Id,
                    userProfile.AuthorizationToken,
                    new TweetOptions
                    {
                        Query = streamFilter.Query,
                        Message = message,
                        StreamedTweetId = streamedTweet.Id,
                        InReplyToStatusId = streamedTweet.StatusId
                    });
            }
            
            //toDo: Remove this code later:
            DomainEvents.Raise(new DisplayActivityOnMap(
                                streamedTweet.StatusId,
                                streamedTweet.UserId,
                                streamedTweet.UserName, 
                                location,
                                dateSent,
                                streamedTweet.Text));

            //var actionsFactory = new StreamingEventActionsFactory();
            //foreach (var factory in streamFilter.StreamingEvents
            //                        .Select(streamingEvent => actionsFactory.Create(streamingEvent.Code.ToString())))
            //{
            //    DomainEvents.Raise(factory.Create(streamedTweet,location, dateSent));
            //}
        }
    }
}
