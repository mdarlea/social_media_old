using Should;
using Swaksoft.Application.Communicator.Dto;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Infrastructure.Crosscutting.Communication.RestClient;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Swaksoft.Communicator.IntegrationTests
{
    [Binding]
    public class CallAndSayVerificationCodeSteps
    {
        public class GivenParameters
        {
            public string TwilioUrl { get; set; }
            public int VerificationCodeLength { get; set; }
        }

        private GivenParameters _givenParameters;
        private MessageOperationResult _result;
        
        [Given(@"the following information:")]
        public void GivenTheFollowingInformation(Table table)
        {

            _givenParameters = table.CreateInstance<GivenParameters>();
        }


        [When(@"I call the member at '(.*)'")]
        public void WhenICallTheMemberAt(string phoneNumber)
        {
            var client = new RestRequestAdapter(new UriBuilder(_givenParameters.TwilioUrl), new HttpClientRequest());
            _result = client.Post(new TwillioTests.TwilioMessage
            {
                ToPhoneNumber = phoneNumber,
                VerificationCodeLength =  _givenParameters.VerificationCodeLength
            }).AndReturn<MessageOperationResult>();
        }

        [Then(@"A new verification code with a length equal to (.*) should be generated")]
        public void ThenANewVerificationCodeWithALengthEqualToShouldBeGenerated(int verificationCodeLentgh)
        {
            _result.ShouldNotBeNull();
            _result.Status.ShouldEqual(ActionResultCode.Success);
            _result.CallId.ShouldNotBeNull();
            _result.CallId.ShouldNotBeEmpty();
        }

        [Then(@"the newly created verification code should be communicated to the applicant")]
        public void ThenTheNewlyCreatedVerificationCodeShouldBeCommunicatedToTheApplicant()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
