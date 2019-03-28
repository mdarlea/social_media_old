using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Should;
using Swaksoft.Application.SocialMedia.Dto;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Core.External;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Application.SocialMedia.Tests.Steps
{
    [Binding]
    public class UserSteps
    {
        private readonly DataContext _context;
        private CollectionActionResult<UserProfile> _userProfilesResult;
        private StreamFilterResult _streamFilterResult;
        private MessageOperationResult _messageOperationResult;
        private CollectionActionResult<StreamFilter> _streamFilterCollectionResult;

        public UserSteps(DataContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
        }

        [Given(@"the following information about the current user:")]
        public void GivenTheFollowingInformationAboutTheCurrentUser(Table table)
        {
            _context.UserProfileRequest = table.CreateInstance<UserProfileRequest>();
        }

        [When(@"I search for the given user's external profiles")]
        public void WhenISearchForTheGivenUserSExternalProfiles()
        {
            using (var service = _context.GetUserAppService())
            {
                _userProfilesResult = service.GetUserProfiles(_context.UserProfileRequest);
            }
        }

        [Then(@"the result should contain one Twitter profile")]
        public void ThenTheResultShouldContainOneTwitterProfile()
        {
            _userProfilesResult.ShouldNotBeNull();
            _userProfilesResult.Status.ShouldEqual(ActionResultCode.Success);
            _userProfilesResult.Items.Count.ShouldBeGreaterThanOrEqualTo(1);

            var twitterProfiles = _userProfilesResult.Items.Where(p => p.ProviderKey == ExternalProvider.Twitter.ToString()).ToList();
            twitterProfiles.Count.ShouldEqual(1);
        }

        [When(@"I request to add the stream filter for this user")]
        public void WhenIRequestToAddTheStreamFilterForThisUser()
        {
            using (var service = _context.GetUserAppService())
            {
                _streamFilterResult = service.AddNewStreamFilter(_context.StreamFilterRequest);
            }
        }

        [Then(@"the result should be successful")]
        public void ThenTheResultShouldBeSuccessful()
        {
            _streamFilterResult.ShouldNotBeNull();
            _streamFilterResult.Status.ShouldEqual(ActionResultCode.Success);
        }

        [Given(@"the following message operation request:")]
        public void GivenTheFollowingMessageOperationRequest(Table table)
        {
            _context.MessageOperationRequest = table.CreateInstance<MessageOperationRequest>();
        }


        [When(@"I request to associate the message to the stream filter")]
        public void WhenIRequestToAssociateTheMessageToTheStreamFilter()
        {
            using (var service = _context.GetUserAppService())
            {
                _messageOperationResult = service.AssociateMessageToStreamFilter(_context.MessageOperationRequest);
            }
        }

        [Then(@"the message operation result should be successful")]
        public void ThenTheMessageOperationResultShouldBeSuccessful()
        {
            _messageOperationResult.ShouldNotBeNull();
            _messageOperationResult.Status.ShouldEqual(ActionResultCode.Success);
        }

        [Given(@"the following stream filter request:")]
        public void GivenTheFollowingStreamFilterRequest(Table table)
        {
            _context.StreamFilterRequest = table.CreateInstance<StreamFilterRequest>();
        }

        [When(@"I request the stream filters for the given user")]
        public void WhenIRequestTheStreamFiltersForTheGivenUser()
        {
            using (var service = _context.GetUserAppService())
            {
                _streamFilterCollectionResult = service.GetStreamFilters(_context.StreamFilterRequest);
            }
        }

        [Then(@"the following stream filters must be returned:")]
        public void ThenTheFollowingStreamFiltersMustBeReturned(Table table)
        {
            var result = GetStreamFiltersFromTable(table);

            result.Status.ShouldEqual(_streamFilterCollectionResult.Status);
            result.Items.Count.ShouldEqual(_streamFilterCollectionResult.Items.Count);

            foreach (var item in _streamFilterCollectionResult.Items)
            {
                var expectedItem = result.Items.SingleOrDefault(i => i.Id == item.Id);
                expectedItem.ShouldNotBeNull();

                if (expectedItem == null) continue;

                item.Query.ShouldEqual(expectedItem.Query);
                item.MessageOperations.Count.ShouldEqual(expectedItem.MessageOperations.Count);

                foreach (var messageOperation in item.MessageOperations)
                {
                    var expectedMessageOperation = 
                        expectedItem.MessageOperations
                            .SingleOrDefault(mo => mo.MessageOperationId == messageOperation.MessageOperationId);
                    
                    expectedMessageOperation.ShouldNotBeNull();

                    if (expectedMessageOperation != null)
                        messageOperation.MessageId.ShouldEqual(expectedMessageOperation.MessageId);
                }
            }
        }

        private static CollectionActionResult<StreamFilter> GetStreamFiltersFromTable(Table table)
        {
            var result = new CollectionActionResult<StreamFilter>
            {
                Status = ActionResultCode.Success
            };

            StreamFilter filter = null;
            foreach (var row in table.Rows)
            {
                var id = row["Id"];
                if (!string.IsNullOrWhiteSpace(id))
                {
                    if (filter != null)
                    {
                        result.Items.Add(filter);
                    }
                    else
                    {
                        filter = new StreamFilter
                        {
                            Id = Convert.ToInt32(id), 
                            Query = row["Query"]
                        };
                    }
                }

                if (filter != null)
                {
                    filter.MessageOperations.Add(new MessageOperation
                    {
                        MessageOperationId = Convert.ToInt32(row["MessageOperation.MessageOperationId"]),
                        MessageId = Convert.ToInt32(row["MessageOperation.MessageId"])
                    });
                }
            }

            if (filter != null)
            {
                result.Items.Add(filter);
            }

            return result;
        }
    }
}
