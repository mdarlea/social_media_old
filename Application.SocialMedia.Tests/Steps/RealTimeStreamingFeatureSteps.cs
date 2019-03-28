using System;
using System.Linq;
using Application.SocialMedia.Tests.Data;
using Should;
using Swaksoft.Application.SocialMedia.Dto;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Swaksoft.Domain.SocialMedia.SocialModule.Events;
using Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories;
using TechTalk.SpecFlow;

namespace Application.SocialMedia.Tests.Steps
{
    [Binding]
    public class RealTimeStreamingFeatureSteps
    {
        private readonly DataContext _context;
        private TweetResult _tweetResult;
        private string _tweetMessage;
        private string _query;

        public RealTimeStreamingFeatureSteps(DataContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
        }

        [When(@"this user subscribes to receive real time notifications")]
        public void WhenThisUserSubscribesToReceiveRealTimeNotifications()
        {
            foreach (var request in _context.OAuthRequests)
            {
                using (var service = _context.GetTwitterStreamingAppService())
                {
                    service.SubscribeForStreaming(request);
                }    
            }
        }

        [When(@"later '(.*)' tweets a message that contains the '(.*)' word")]
        public void WhenLaterATwitterUserTweetsAMessageThatContainsTheWord(string user, string word)
        {
            OAuthToken token=null;
            _tweetMessage = string.Format("{0} {1}", word, Guid.NewGuid());
            _query = word;
            var userProfileId = 0;

            //get the first user that subscribed for twitter
            var uofw = new ProfiledSocialMediaUnitOfWork();
            using (var repository = new UserProfileRepository(uofw))
            {
                var profile = repository.GetAll().OfType<TwitterUserProfile>().SingleOrDefault(u => u.UserName == user);
                
                if (profile != null)
                {
                    userProfileId = profile.Id;
                    token = profile.AuthorizationToken;
                }
            }

            //sends the tweet
            var oauthRequest = _context.OAuthRequests.FirstOrDefault();
            if (oauthRequest == null)
            {
                throw new Exception("Could not find the Twitter application credentials");
            }

            var service = _context.GetMessageSenderServiceAgent();
            _tweetResult = service.SendMessage(
                oauthRequest.ClientCredentials, 
                userProfileId, 
                token,
                new TweetOptions
                {
                    Query = _query,
                    Message = _tweetMessage
                });
            System.Threading.Thread.Sleep(200);
        }

        [Then(@"the associated tweet streamed event should be fired")]
        public void ThenTheAssociatedTweetStreamedEventShouldBeFired()
        {
            _tweetResult.ShouldNotBeNull();
            _tweetResult.Status.ShouldEqual(ActionResultCode.Success);

            System.Threading.Thread.Sleep(2);

            var raisedEvents = BeforeTestRunHooks.GetRaisedDomanEvents().OfType<TweetStreamed>();
            var events = raisedEvents.Where(e => e.Query == _query).ToList();
            events.Count.ShouldEqual(_context.OAuthRequests.Count);

            foreach(var @event in events)
            {
                @event.ShouldNotBeNull();
                @event.Text.ShouldEqual(_tweetMessage);                
            }
        }
    }
}
