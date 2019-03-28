using Swaksoft.Application.Communicator.Dto;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swaksoft.Application.Communicator.MessagingModule.Services;
using Swaksoft.Application.Communicator.MessagingModule.Services.Providers.Twilio;
using Twilio.WebApi;

namespace Swaksoft.Communicator.Host.Controllers
{
    public class TwimlController : ApiController
    {
        private readonly IXmlVoiceMessagingAppService _xmlVoiceMessagingService;
        
        public TwimlController(IXmlVoiceMessagingAppService xmlVoiceMessagingService)
        {
            if (xmlVoiceMessagingService == null) throw new ArgumentNullException("xmlVoiceMessagingService");
            _xmlVoiceMessagingService = xmlVoiceMessagingService;
        }

        public HttpResponseMessage VerificationCode([FromBody] VoiceRequest twilioRequest)
        {
            var digits = twilioRequest.Digits;

            var option = 0;
            var hasOption = !string.IsNullOrWhiteSpace(digits);
            if (hasOption)
            {

                int.TryParse(digits, out option);
            }

            var result = _xmlVoiceMessagingService.VerificationCode(new VerificationCodeRequest
            {
                Option = (hasOption) ? option : (int?)null,
                CallSid = twilioRequest.CallSid
            });

            return Request.CreateResponse(HttpStatusCode.OK, result.XmlResponse);
        }
    }
}
