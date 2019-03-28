using System;
using System.Linq;
using System.Xml.Linq;
using Swaksoft.Application.Communicator.Dto;
using Swaksoft.Application.Communicator.Resources;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Services;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Application.Communicator.MessagingModule.Services.Providers.Twilio
{
    public class TwimlVoiceMessagingAppService : CommunicatorAppServiceBase<TwimlVoiceMessagingAppService>, IXmlVoiceMessagingAppService
    {
        private readonly IRepository<VoiceOperation> _voiceOperationRepository;
        private readonly IRepository<CommunicationLog> _communicationLogRepository;
        private readonly IRepository<TwilioProfile> _twilioProfileRepository;
        private readonly IVoiceOperationsService _voiceOperationsService;

        public TwimlVoiceMessagingAppService(
            IRepository<VoiceOperation> voiceOperationRepository,
            IRepository<CommunicationLog> communicationLogRepository,
            IRepository<TwilioProfile> twilioProfileRepository, 
            IVoiceOperationsService voiceOperationsService)
        {
            if (voiceOperationRepository == null) throw new ArgumentNullException("voiceOperationRepository");
            if (communicationLogRepository == null) throw new ArgumentNullException("communicationLogRepository");

            if (twilioProfileRepository == null) throw new ArgumentNullException("twilioProfileRepository");

            if (voiceOperationsService == null) throw new ArgumentNullException("voiceOperationsService");
            _voiceOperationRepository = voiceOperationRepository;
            _communicationLogRepository = communicationLogRepository;
            _twilioProfileRepository = twilioProfileRepository;
            _voiceOperationsService = voiceOperationsService;
        }

        public XmlActionResult VerificationCode(VerificationCodeRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (string.IsNullOrWhiteSpace(request.CallSid)) 
                throw new ArgumentException(Messages.api_CallIdCannotBeNull, "request");

            try
            {
                var deliveredMessage = GetVoiceDeliveredMessage(request.CallSid);
                if (deliveredMessage == null)
                {
                    return MissingVoiceCall<XmlActionResult>(request.CallSid);
                }

                //get the configuration and the configuratin code
                var twilioProfile = _twilioProfileRepository.GetAll().FirstOrDefault();
                if (twilioProfile == null)
                {
                    return MisssingConfiguration<XmlActionResult>("Twilio");
                }

                return SayWord(request.Option, twilioProfile,deliveredMessage);
            }
            catch (Exception ex)
            {
                GetLog().Fatal(ex.Message, ex);
                return new XmlActionResult
                {
                    Status = ActionResultCode.Failed,
                    Message = ex.Message
                };
            }
        }

        private VoiceCallLog GetVoiceDeliveredMessage(string callSid)
        {
            //ToDo: wait for the call record created event here
            var voiceCall = _communicationLogRepository.GetSingle(c => c.CallId == callSid) as VoiceCallLog;
            return voiceCall;
        }

        private XmlActionResult SayWord(int? option, CommunicatorProfile twilioProfile, VoiceCallLog call)
        {
            XElement result;

            using (var transaction = _voiceOperationRepository.BeginTransaction())
            {
                result = _voiceOperationsService.SayVerificationCodeMessage(twilioProfile, option, call);

                //saves the attempt
                transaction.Commit();    
            }

            return new XmlActionResult
            {
                Status = ActionResultCode.Success,
                XmlResponse = result
            };
        }

        #region dispose
        protected override void Dispose(bool disposing)
        {
            if (_communicationLogRepository != null)
            {
                _communicationLogRepository.Dispose();
            }
            if (_voiceOperationRepository != null)
            {
                _voiceOperationRepository.Dispose();
            }
        }
        #endregion dispose
    }
}
