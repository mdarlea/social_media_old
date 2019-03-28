using System;
using System.Linq;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Application.SocialMedia.Resources;
using Swaksoft.Application.SocialMedia.SocialModule.Providers;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Events;
using TweetSent = Swaksoft.Application.SocialMedia.Dto.TweetSent;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public class UserProfileAppService : AppServiceBase<UserProfileAppService>, IUserProfileAppService
    {
        private readonly ActionsFactory<IProviderFactory> _actionsFactory;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ISentMessageRepository _sentMessageRepository;

        public UserProfileAppService(
            ActionsFactory<IProviderFactory> actionsFactory,
            IUserProfileRepository userProfileRepository,
            ISentMessageRepository sentMessageRepository)
        {
            if (actionsFactory == null) throw new ArgumentNullException("actionsFactory");
            if (userProfileRepository == null) throw new ArgumentNullException("userProfileRepository");
            if (sentMessageRepository == null) throw new ArgumentNullException("sentMessageRepository");

            _actionsFactory = actionsFactory;
            _userProfileRepository = userProfileRepository;
            _sentMessageRepository = sentMessageRepository;
        }

        public ActionResult AddNewSentTweet(TweetSent request)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (request.UserProfileId < 1) throw new ArgumentNullException("request", @"Invalid UserProfileId");


                //get the user profile
                var spec = UserProfileSpecifications.UserProfileById<TwitterUserProfile>(request.UserProfileId);

                var userProfile = _userProfileRepository.AllMatching(spec).SingleOrDefault();
                if (userProfile == null)
                {
                    return Error<ActionResult>(Messages.api_MissingUserProfile, Messages.const_Twitter, request.UserProfileId);
                }

                var factory = new SentTweetFactory(
                    userProfile,
                    request.DateSent, request.Text,
                    request.StatusId, request.SentByUserId, request.SentByUserName, request.InReplyToStatusId,
                    request.SentToUserId, request.SentToUserName, request.StreamedTweetId);
                var sentTweet = factory.CreateSentMessage();

               _sentMessageRepository.SaveEntity(sentTweet);
                
                //returns success
                return new ActionResult
                {
                    Status = ActionResultCode.Success
                };
        }

        public ActionResult RemoveUserProfile(Dto.UserProfileRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (string.IsNullOrWhiteSpace(request.UserId)) throw new ArgumentNullException("request", @"Null UserId");

                var result = new ActionResult
                {
                    Status = ActionResultCode.Errored
                };

                var factory = _actionsFactory.Resolve(request.Type.ToString());
                var profile = factory.CreateUserProfile(_userProfileRepository, request.UserId);

                if (profile == null)
                {
                    result.Message = string.Format(@"Could not find a user profile for {0}", request.UserId);
                    return result;
                }

                var userProfileId = profile.Id;
                using (var transaction = _userProfileRepository.BeginTransaction())
                {
                    _userProfileRepository.Remove(profile);

                    transaction.Commit();
                }

                DomainEvents.Raise(new UserProfileRemoved(userProfileId));

                //returns success
                return new ActionResult
                {
                    Status = ActionResultCode.Success
                };
         }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_userProfileRepository != null)
            {
                _userProfileRepository.Dispose();
            }
            if (_sentMessageRepository != null)
            {
                _sentMessageRepository.Dispose();
            }
        }
    }

}
