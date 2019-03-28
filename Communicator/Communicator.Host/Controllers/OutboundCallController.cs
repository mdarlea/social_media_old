using System;
using System.Web.Http;
using System.Web.Http.Description;
using Swaksoft.Application.Communicator.MessagingModule.Services;
using Swaksoft.Application.Communicator.Dto;

namespace Swaksoft.Communicator.Host.Controllers
{
    public class OutboundCallController : ApiController
    {
        private readonly ICommunicatorAppService _outboundCallAppService;

        public OutboundCallController(ICommunicatorAppService outboundCallAppService)
        {
            if (outboundCallAppService == null) throw new ArgumentNullException("outboundCallAppService");
            _outboundCallAppService = outboundCallAppService;
        }

        // POST api/twilio
        [ResponseType(typeof(MessageOperationResult))]
        public IHttpActionResult VerificationCode(VerificationCode verificationCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _outboundCallAppService.VerificationCode(verificationCode);
            return Ok(result);
        }

        #region disposing
        protected override void Dispose(bool disposing)
        {
            if (_outboundCallAppService != null)
            {
                _outboundCallAppService.Dispose();    
            }
            base.Dispose(disposing);
        }
        #endregion disposing
    }
}
