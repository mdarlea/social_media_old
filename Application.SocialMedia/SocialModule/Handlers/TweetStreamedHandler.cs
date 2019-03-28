using System;
using System.Linq;
using System.Transactions;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Domain.Seedwork.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Services;

namespace Swaksoft.Application.SocialMedia.SocialModule.Handlers
{
    public class TweetStreamedHandler : HandlerBase<TweetStreamed>
    {
        private readonly IStreamFilterRepository _streamFilterRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IStreamedTweetRepository _streamedTweetRepository;
        private readonly ITweetProcessorService _tweetProcessorService;


        public TweetStreamedHandler(
            IStreamFilterRepository streamFilterRepository,
            IUserProfileRepository userProfileRepository,
            IStreamedTweetRepository streamedTweetRepository,
            ITweetProcessorService tweetProcessorService)
        {
            if (userProfileRepository == null) throw new ArgumentNullException("userProfileRepository");
            if (streamedTweetRepository == null) throw new ArgumentNullException("streamedTweetRepository");
            if (tweetProcessorService == null) throw new ArgumentNullException("tweetProcessorService");

            _streamFilterRepository = streamFilterRepository;
            _userProfileRepository = userProfileRepository;
            _streamedTweetRepository = streamedTweetRepository;
            _tweetProcessorService = tweetProcessorService;
        }

        public override void Handle(TweetStreamed args)
        {
            if (args == null) throw new ArgumentNullException("args");
            if (args.UserProfileId < 1) throw new ArgumentNullException("args", @"Invalid UserProfileId");
            if (string.IsNullOrWhiteSpace(args.Query)) throw new ArgumentNullException("args", @"Query cannot be null");

            //get the user profile
            var spec = UserProfileSpecifications.UserProfileById<TwitterUserProfile>(args.UserProfileId);
            var userProfile = _userProfileRepository.AllMatching(spec).SingleOrDefault();

            if (userProfile == null)
                throw new Exception(string.Format("Could not find profile for userId={0}", args.UserProfileId));

            //add the streamed tweet to the database
            var tweet = StreamedTweetFactory.CreateStreamedTweet(
                args.Query,
                args.StatusId, args.SentByUserId, args.SentByUserName, args.Name, args.Text,
                args.InReplyToStatusId, args.ProfileImageUrl, args.ProfileImageUrlHttps);

            _streamedTweetRepository.SaveEntity(tweet);
            
            //process the tweet
            var userId = userProfile.User.Id;
            var specification = StreamFilterSpecifications.StreamFiltersByQuery(userId, args.Query);
            var streamFilter = _streamFilterRepository.GetSingle(specification.SatisfiedBy());
            if (streamFilter == null)
            {
                throw new Exception(string.Format("Could not find a filter '{0}' for UserId={1}",args.Query,userId));
            }
            _tweetProcessorService.ProcessStreamFilter(
                userProfile,
                streamFilter,
                args.ClientCredentials,
                tweet, 
                args.Location,
                args.DateSent);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            if (_userProfileRepository != null)
            {
                _userProfileRepository.Dispose();
            }
            if (_streamedTweetRepository != null)
            {
                _streamedTweetRepository.Dispose();
            }
            _streamFilterRepository.Dispose();
        }
    }
}
