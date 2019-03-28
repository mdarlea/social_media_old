using System.Net;
using System.Net.Http;
using System.Web.Http;
using Twilio.TwiML;
using Twilio.TwiML.WebApi;
using Twilio.WebApi;

namespace Swaksoft.Communicator.Host.Controllers
{
    public class TestVoiceController : TwilioController
    {
        public HttpResponseMessage CallMe([FromBody] VoiceRequest twilioRequest)
        {
            var twilioResponse = new TwilioResponse();
            twilioResponse.Say("Michelle is testing for performance");
            return Request.CreateResponse(HttpStatusCode.OK, twilioResponse.Element);
        }
    }
}
