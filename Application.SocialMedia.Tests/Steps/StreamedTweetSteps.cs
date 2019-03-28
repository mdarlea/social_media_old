using System;
using System.Linq;
using Application.SocialMedia.Tests.Data;
using Should;
using Swaksoft.Application.SocialMedia.Dto;
using Swaksoft.Application.SocialMedia.SocialModule.Services;
using Swaksoft.Infrastructure.Crosscutting.Caching;
using Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Application.SocialMedia.Tests.Steps
{
    [Binding]
    public class StreamedTweetSteps
    {
        private int _page;
        private StreamedTweetOptions _request;
        private PagedListActionResult _results;

        [Given(@"I want to see streamed tweets on the (.*)st page of the screen")]
        public void GivenIWantToSeeStreamedTweetsOnTheStPageOfTheScreen(int page)
        {
            _page = page;
        }

        [Given(@"the page size is (.*)")]
        public void GivenThePageSizeIs(int pageSize)
        {
            _request.PageSize = pageSize;
        }

        [Given(@"the tweets must match the following search criteria")]
        public void GivenTheTweetsMustMatchTheFollowingSearchCriteria(Table table)
        {
            _request = table.CreateInstance<StreamedTweetOptions>();
        }

        [When(@"I search for the streamed tweets")]
        public void WhenISearchForTheStreamedTweets()
        {
            var uofw = new ProfiledSocialMediaUnitOfWork();

            using (var service = new StreamedTweetAppService(
                new StreamedTweetRepository(uofw),
                new UserProfileRepository(uofw),
                CacheProviderFactory.Provider))
            {
                _results = service.FindStreamedTweets(_request);
            }
        }

        [Then(@"the found tweets must be displayed on the screen")]
        public void ThenTheFoundTweetsMustBeDisplayedOnTheScreen()
        {
            _results.ShouldNotBeNull();
            _results.Items.Content.ToList().Count.ShouldBeLessThanOrEqualTo(_request.PageSize);
            _results.Items.CurrentPage.ShouldEqual(_page);
            _results.Items.PageSize.ShouldEqual(_request.PageSize);
        }

    }
}
