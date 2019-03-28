using System;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.Communicator.MessagingModule.Contracts;
using Twilio;

namespace AntiCorruption.Twillio.Extensions
{
    public static class RestExceptionExtensions
    {
        public static TResult ToProviderActionResult<TResult>(this TwilioBase twilioResponse, Action<TResult> callback)
            where TResult : ProviderActionResult,new()
        {
            if (twilioResponse == null) throw new ArgumentNullException("twilioResponse");
            if (callback == null) throw new ArgumentNullException("callback");
            var result = new TResult();

            if (twilioResponse.RestException != null)
            {
                result.Code = twilioResponse.RestException.Code;
                result.MoreInfo = twilioResponse.RestException.MoreInfo;
                result.Message = twilioResponse.RestException.Message;
                result.Status = ActionResultCode.Failed;
            }
            else
            {
                result.Status = ActionResultCode.Success;
            }

            callback(result);

            return result;
        }
    }
}
