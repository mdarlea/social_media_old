using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using Swaksoft.Application.Communicator.Dto;
using Swaksoft.Core.Dto;
using Swaksoft.Infrastructure.Crosscutting.Communication.RestClient;

namespace Swaksoft.Communicator.IntegrationTests
{
    [TestClass]
    [Ignore]
    public class TwillioTests
    {
        private string _url;
        private string _phoneNumber;
        private MessageOperationResult _result;

        public class TwilioMessage
        {
            public string ToPhoneNumber { get; set; }
            public int VerificationCodeLength { get; set; }
        }

        [TestMethod]
        public void SayValidationCodeTest()
        {
            GivenTheTwillioApiUrl("http://mdarlea.azurewebsites.net/api/OutboundCall/SayWord.json/");
            AndAPhoneNumber("813-340-6560");
            AndANewVerificationCode("154378");
            WhenICallThePhoneNumber();
            ThenTheCallShouldBeSuccessfull();
        }

        private void ThenTheCallShouldBeSuccessfull()
        {
            _result.ShouldNotBeNull();
            _result.Status.ShouldEqual(ActionResultCode.Success);
            //_result.VerificationCode.ShouldEqual(_verificationCode);
            _result.CallId.ShouldNotBeNull();
            _result.CallId.ShouldNotBeEmpty();
        }

        private void WhenICallThePhoneNumber()
        {
            var jsonRequest = new HttpClientRequest();
            var uriBuilder = new UriBuilder(_url);
            var client = new RestRequestAdapter(uriBuilder, jsonRequest);
            _result = client.Post(new TwilioMessage
            {
                ToPhoneNumber = _phoneNumber,
                VerificationCodeLength = 7
            }).AndReturn<MessageOperationResult>();
        }

        private void AndANewVerificationCode(string verificationCode)
        {
        }

        private void AndAPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        private void GivenTheTwillioApiUrl(string url)
        {
            _url = url;
        }
    }
}
