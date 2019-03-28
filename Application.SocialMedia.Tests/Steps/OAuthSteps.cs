using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Application.SocialMedia.Tests.Data;
using Swaksoft.Application.SocialMedia.Dto;
using Swaksoft.Application.SocialMedia.SocialModule.Services;
using Swaksoft.Core;
using Swaksoft.Core.External;
using Swaksoft.Infrastructure.AntiCorruption.Twitter.Services;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Application.SocialMedia.Tests.Steps
{
    [Binding]
    public class OAuthSteps
    {
        private readonly DataContext _dataContext;

        public OAuthSteps(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [Given(@"the following information:")]
        public void GivenTheFollowingInformation(Table table)
        {
            var requests = table.CreateSet<OAuthRequest>().ToList();
            var i = 0;
            foreach (var row in table.Rows)
            {
                var typeValue = row["ClientCredentials.Type"];
                var consumerKey = row["ClientCredentials.ConsumerKey"];
                var consumerSecret = row["ClientCredentials.ConsumerSecret"];

                if (typeValue != null && consumerKey != null && consumerSecret != null)
                {
                    var request = requests[i];
                    i++;

                    ExternalProvider type;
                    Enum.TryParse(typeValue, out type);

                    request.ClientCredentials = new ExternalProviderCredentials(type, consumerKey, consumerSecret);
                }    
            }

            _dataContext.OAuthRequests = requests;
        }


        [When(@"I request authorization")]
        public void WhenIRequestAuthorization()
        {
            var uofw = new ProfiledSocialMediaUnitOfWork();

            using (var service = new OAuthAppService(new OAuthAuthorizationAdapter()))
            {
                //var result = service.GetUserProfile(_request);
                using (var trans = new TransactionScope())
                {
                    var result = service.Authorize(_dataContext.OAuthRequests.FirstOrDefault());
                }
            }
        }

        [Then(@"the Twitter user profile should be returned")]
        public void ThenTheTwitterUserProfileShouldBeReturned()
        {
            
        }
    }
}
