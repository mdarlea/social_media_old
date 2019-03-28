using Swaksoft.Application.Communicator.Resources;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.Communicator.MessagingModule.Services
{
    public class CommunicatorAppServiceBase<T> : AppServiceBase<T>
    {
        protected static TResult MisssingConfiguration<TResult>(string configurationName)
            where TResult:ActionResult,new()
        {
            var actionResult = new TResult
            {
                Status = ActionResultCode.Failed,
                Message = string.Format(Messages.api_MissingConfiguration,configurationName)
            };
            GetLog().LogError(actionResult.Message);
            return actionResult;
        }

        protected static TResult MissingVoiceOperation<TResult>(string action)
            where TResult : ActionResult, new()
        {
            var actionResult = new TResult
            {
                Status = ActionResultCode.Failed,
                Message = string.Format(Messages.api_MissingVoiceOperation,action)
            };
            GetLog().LogError(actionResult.Message);
            return actionResult;
        }

        protected static TResult MissingVoiceCall<TResult>(string callId)
            where TResult : ActionResult, new()
        {
            var actionResult = new TResult
            {
                Status = ActionResultCode.Failed,
                Message = string.Format(Messages.api_MissingVoiceCall, callId)
            };
            GetLog().LogError(actionResult.Message);
            return actionResult;
        }

        protected static TResult MissingSmsPhoneNumber<TResult>()
          where TResult : ActionResult, new()
        {
            var actionResult = new TResult
            {
                Status = ActionResultCode.Failed,
                Message = string.Format(Messages.api_MissingSmsPhoneNumber)
            };
            GetLog().LogError(actionResult.Message);
            return actionResult;
        }
    }
}
