using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Infrastructure.Crosscutting.Caching;
using Swaksoft.Infrastructure.Crosscutting.Extensions;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public class StreamedTweetAppService : AppServiceBase<StreamedTweetAppService>, IStreamedTweetAppService
    {
        private readonly IStreamedTweetRepository _streamedTweetRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICacheProvider _cacheProvider;

        public StreamedTweetAppService(
            IStreamedTweetRepository streamedTweetRepository, 
            IUserProfileRepository userProfileRepository,
            ICacheProvider cacheProvider)
        {
            if (streamedTweetRepository == null) throw new ArgumentNullException("streamedTweetRepository");
            if (userProfileRepository == null) throw new ArgumentNullException("userProfileRepository");

            _streamedTweetRepository = streamedTweetRepository;
            _userProfileRepository = userProfileRepository;
            _cacheProvider = cacheProvider;
        }

        public Dto.PagedListActionResult FindStreamedTweets(Dto.StreamedTweetOptions request)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (string.IsNullOrWhiteSpace(request.UserId)) throw new ArgumentNullException("request", @"Invalid UserId");

                var userProfileId = GetTwitterUserProfile(request.UserId);
                
                if (userProfileId < 1)
                {
                    return new Dto.PagedListActionResult
                    {
                        Status = ActionResultCode.Errored,
                        Message = String.Format("Could not find a user profile for UserId='{0}'", request.UserId)
                    };
                }

                var options = request.ProjectedAs<StreamedTweetOptions>();
                options.UserProfileId = userProfileId;
                
                var spec = StreamedTweetSpecifications.DynamicFilteredStreamedTweets(options);

                var tweets = _streamedTweetRepository
                    .GetTweets()
                    .GetPaged(request.PageSize, t => t.StreamedTweet.Id, request.MinIdentity??0, spec.Filter, spec.Parameters);

                //returns success
                return new Dto.PagedListActionResult
                {
                    Status = ActionResultCode.Success,
                    Items = new PagedList
                    {
                        CurrentPage = tweets.CurrentPage,
                        PageSize = tweets.PageSize,
                        TotalRecords = tweets.TotalRecords,
                        Content = (from t in tweets.Content select ((StreamedTweetsFilter) t).StreamedTweet).ProjectedAsCollection<Dto.StreamedTweet>()
                    }
                };
        }

        private int GetTwitterUserProfile(string userId)
        {
            //get the user profile
            var key = string.Format("{0}-{1}", typeof(TwitterUserProfile).Name, userId);

            var userProfileIdVal = _cacheProvider.Get(key,
                () =>
                {
                    var userProfile = _userProfileRepository.GetByUserId<TwitterUserProfile>(userId).SingleOrDefault();
                    return (userProfile == null) ? null : userProfile.Id.ToString();
                },
                DateTime.Today.AddDays(1));

            int userProfileId;
            var result = int.TryParse(userProfileIdVal, out userProfileId);

            return (result) ? userProfileId : 0;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            if (_streamedTweetRepository != null)
            {
                _streamedTweetRepository.Dispose();
            }
        }
    }
}
