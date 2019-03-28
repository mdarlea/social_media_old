using System;
using System.Web.Http;
using System.Web.Http.Description;
using Swaksoft.Application.Communicator.MessagingModule.Services;
using Swaksoft.Application.Communicator.Dto;

namespace Swaksoft.Communicator.Host.Controllers
{
    public class SmsController : ApiController
    {
        private readonly ICommunicatorAppService _smsAppService;

        public SmsController(ICommunicatorAppService smsAppService)
        {
            if (smsAppService == null) throw new ArgumentNullException("smsAppService");
            _smsAppService = smsAppService;
        }

        [ResponseType(typeof(MessageOperationResult))]
        public IHttpActionResult VerificationCode(VerificationCode verificationCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _smsAppService.VerificationCode(verificationCode);
            return Ok(result);
        }

        #region disposing
        protected override void Dispose(bool disposing)
        {
            if (_smsAppService != null)
            {
                _smsAppService.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion disposing
    }
}
