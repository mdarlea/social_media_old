using System;
using Swaksoft.Application.SocialMedia.SocialModule.Services;
using Swaksoft.Domain.Seedwork.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Events;
using Swaksoft.Infrastructure.Crosscutting.Extensions;

namespace Swaksoft.Application.SocialMedia.SocialModule.Handlers
{
    public class TweetSentHandler : HandlerBase<MessageSent>
    {
        private readonly IUserProfileAppService _userProfileAppService;

        public TweetSentHandler(IUserProfileAppService userProfileAppService)
        {
            if (userProfileAppService == null) throw new ArgumentNullException("userProfileAppService");
            _userProfileAppService = userProfileAppService;
        }

        public override void Handle(MessageSent args)
        {
            if (args == null) throw new ArgumentNullException("args");
            _userProfileAppService.AddNewSentTweet(args.ProjectedAs<Dto.TweetSent>());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            if (_userProfileAppService != null)
            {
                _userProfileAppService.Dispose();    
            }
        }
    }
}
