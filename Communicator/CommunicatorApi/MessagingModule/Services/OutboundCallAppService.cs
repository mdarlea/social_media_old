using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Swaksoft.Application.Communicator.Dto;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Exceptions;
using Swaksoft.Domain.Communicator.MessagingModule.Providers;
using Swaksoft.Domain.Communicator.Resources;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Seedwork.Events;
using VerificationCode = Swaksoft.Domain.Communicator.MessagingModule.Aggregates.VerificationCodeAgg.VerificationCode;
using VerificationCodeSent = Swaksoft.Domain.Communicator.MessagingModule.Events.VerificationCodeSent;

namespace Swaksoft.Application.Communicator.MessagingModule.Services
{
    public class OutboundCallAppService : CommunicatorAppServiceBase<OutboundCallAppService>, ICommunicatorAppService
    {
        private readonly ICommunicatorProviderFactory _providersFactory;
        private readonly IRepository<VerificationCode> _verificationCodeRepository;
        private readonly IRepository<CommunicationLog> _communicationLogRepository;
        private readonly IRepository<VoiceOperation> _voiceOperationRepository;
        private readonly IRepository<CommunicatorProfile> _communicatorProfileRepository;

        public OutboundCallAppService(
            ICommunicatorProviderFactory providersFactory,
            IRepository<VerificationCode> verificationCodeRepository,
            IRepository<CommunicationLog> communicationLogRepository, 
            IRepository<VoiceOperation> voiceOperationRepository,
            IRepository<CommunicatorProfile> communicatorProfileRepository)
        {
            if (providersFactory == null) throw new ArgumentNullException("providersFactory");
            if (verificationCodeRepository == null) throw new ArgumentNullException("verificationCodeRepository");
            if (communicationLogRepository == null) throw new ArgumentNullException("communicationLogRepository");
            if (voiceOperationRepository == null) throw new ArgumentNullException("voiceOperationRepository");
            if (communicatorProfileRepository == null) throw new ArgumentNullException("communicatorProfileRepository");

            _providersFactory = providersFactory;
            _verificationCodeRepository = verificationCodeRepository;
            _communicationLogRepository = communicationLogRepository;
            _voiceOperationRepository = voiceOperationRepository;
            _communicatorProfileRepository = communicatorProfileRepository;
        }

        public MessageOperationResult VerificationCode(Dto.VerificationCode message)
        {
            if (message == null) throw new ArgumentNullException("message");

            try
            {
                //get the configuration and the configuratin code
                var communicatorProfile = _communicatorProfileRepository.GetFiltered(p => p.Name == "Default").FirstOrDefault();
                if (communicatorProfile == null)
                {
                    return MisssingConfiguration<MessageOperationResult>("Default");
                }

                //get the action to execute
                var spec = MessageOperationSpecifications.VerificationCodeAction<VoiceOperation>();
                var operation = _voiceOperationRepository.AllMatching(spec).FirstOrDefault();
                if (operation == null)
                {
                    return MissingVoiceOperation<MessageOperationResult>("verification code");
                }

                //generates a new code
                var code = new VerificationCode();
                code.GenerateNewCode(message.VerificationCodeLength);
                _verificationCodeRepository.SaveEntity(code);
                
                dynamic options = new ExpandoObject();
                options.Code = code.Code;
                var log = InitiateOutboundCall(communicatorProfile, operation, message.ToPhoneNumber, (IDictionary<string, object>)options);

                DomainEvents.Raise(new VerificationCodeSent(log)
                {
                    VerificationCode = code,
                    MemberNbr = message.MemberNumber
                });

                _communicationLogRepository.SaveEntity(log);

                return new MessageOperationResult
                {
                    CallId = log.CallId,
                    Status = ActionResultCode.Success
                };
            }
            catch (CommunicationProviderException e)
            {
                _communicationLogRepository.SaveEntity(e.CommunicationLog);

                GetLog().LogError("{0} {1} {2}", e, e.Message, e.Code, e.MoreInfo);
                return new MessageOperationResult
                {
                    Status = ActionResultCode.Failed,
                    Message = e.Message,
                    Code = e.Code
                };       
            }
            catch (Exception ex)
            {
                GetLog().Fatal(ex.Message,ex);
                return new MessageOperationResult
                {
                    Status = ActionResultCode.Failed,
                    Message = ex.Message
                };       
            }
        }
        
        #region private methods
        private VoiceCallLog InitiateOutboundCall(
            CommunicatorProfile communicatorProfile,
            VoiceOperation voiceOperation, string toPhoneNumber, IDictionary<string, object> messageArgs)
        {
            if (communicatorProfile == null) throw new ArgumentNullException("communicatorProfile");
            if (voiceOperation == null) throw new ArgumentNullException("voiceOperation");
            if (messageArgs == null) throw new ArgumentNullException("messageArgs");
            if (string.IsNullOrWhiteSpace(toPhoneNumber)) throw new ArgumentNullException("toPhoneNumber");
            
            //get the from phone number
            var fromPhoneNumber = voiceOperation.FromPhoneNumber ?? communicatorProfile.DefaultPhoneNumber;

            //check if we can call the phone number
            PhoneNumber toPhone;
            if (!PhoneNumber.TryParse(toPhoneNumber, out toPhone))
            {
                throw new ArgumentException(Resources.Messages.validation_InvalidPhoneNumber, "toPhoneNumber");
            }

            //get the associated communicator
            var communicatorProvider = _providersFactory.Create<ICommunicatorProvider>(communicatorProfile);

            var result = communicatorProvider.InitiateOutboundCall(voiceOperation, fromPhoneNumber, toPhone, messageArgs);
            return CommunicationLogFactory<VoiceCallLog>.LogMessageResult(result, fromPhoneNumber, toPhone, voiceOperation);
        }
        #endregion private methods

        #region dispose
        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;

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
