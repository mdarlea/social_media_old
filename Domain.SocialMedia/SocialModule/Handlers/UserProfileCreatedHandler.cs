using System;
using Swaksoft.Domain.Seedwork.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Swaksoft.Domain.SocialMedia.SocialModule.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Services;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Handlers
{
    public class UserProfileCreatedHandler : HandlerBase<UserProfileCreated>
    {
        private readonly IMessageSenderService _messageSenderService;

        public UserProfileCreatedHandler(IMessageSenderService messageSenderService)
        {
            if (messageSenderService == null) throw new ArgumentNullException("messageSenderService");
            _messageSenderService = messageSenderService;
        }

        public override void Handle(UserProfileCreated args)
        {
            if (args == null) throw new ArgumentNullException("args");

            if (string.IsNullOrWhiteSpace(args.Message)) return;

            //sends a new tweet
            _messageSenderService.SendMessage(
                args.ClientCredentials,
                args.UserProfileId,
                args.AuthorizationToken,
                new TweetOptions
                {
                  Message  = args.Message
                });
        }
    }
}
