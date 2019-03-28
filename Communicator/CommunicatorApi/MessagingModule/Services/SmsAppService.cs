using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Swaksoft.Application.Communicator.Dto;
using Swaksoft.Application.Communicator.Resources;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Exceptions;
using Swaksoft.Domain.Communicator.MessagingModule.Providers;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Seedwork.Events;
using VerificationCode = Swaksoft.Domain.Communicator.MessagingModule.Aggregates.VerificationCodeAgg.VerificationCode;
using VerificationCodeSent = Swaksoft.Domain.Communicator.MessagingModule.Events.VerificationCodeSent;

namespace Swaksoft.Application.Communicator.MessagingModule.Services
{
    public class SmsAppService : CommunicatorAppServiceBase<SmsAppService>, ICommunicatorAppService
    {
        private readonly ICommunicatorProviderFactory _providersFactory;
        private readonly IRepository<VerificationCode> _verificationCodeRepository;
        private readonly IRepository<CommunicationLog> _communicationLogRepository;
        private readonly IRepository<SmsOperation> _smsOperationRepository;
        private readonly IRepository<CommunicatorProfile> _communicatorProfileRepository;
        private readonly ICommunicatorPhoneNumberRepository _communicatorPhoneNumberRepository;

        public SmsAppService(
            ICommunicatorProviderFactory providersFactory,
            IRepository<VerificationCode> verificationCodeRepository,
            IRepository<CommunicationLog> communicationLogRepository,
            IRepository<SmsOperation> smsOperationRepository,
            IRepository<CommunicatorProfile> communicatorProfileRepository,
            ICommunicatorPhoneNumberRepository communicatorPhoneNumberRepository)
        {
            if (providersFactory == null) throw new ArgumentNullException("providersFactory");
            if (verificationCodeRepository == null) throw new ArgumentNullException("verificationCodeRepository");
            if (communicationLogRepository == null) throw new ArgumentNullException("communicationLogRepository");
            if (smsOperationRepository == null) throw new ArgumentNullException("smsOperationRepository");
            if (communicatorProfileRepository == null) throw new ArgumentNullException("communicatorProfileRepository");
            if (communicatorPhoneNumberRepository == null) throw new ArgumentNullException("communicatorPhoneNumberRepository");


            _providersFactory = providersFactory;
            _verificationCodeRepository = verificationCodeRepository;
            _communicationLogRepository = communicationLogRepository;
            _smsOperationRepository = smsOperationRepository;
            _communicatorProfileRepository = communicatorProfileRepository;
            _communicatorPhoneNumberRepository = communicatorPhoneNumberRepository;
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
                var spec = MessageOperationSpecifications.VerificationCodeAction<SmsOperation>();
                var operation = _smsOperationRepository.AllMatching(spec).FirstOrDefault();
                if (operation == null)
                {
                    return MissingSmsOperation<VerificationCodeResult>("verification code");
                }

                //get the phone number that will send this SMS message
                var fromPhoneNumber = _communicatorPhoneNumberRepository.GetSms();
                if (fromPhoneNumber == null)
                {
                    return MissingSmsPhoneNumber<VerificationCodeResult>();
                }

                //generates a new code
                var code = new VerificationCode();
                code.GenerateNewCode(message.VerificationCodeLength);
                _verificationCodeRepository.SaveEntity(code);
              
                dynamic options = new ExpandoObject();
                options.Code = code.Code;
                var log = SendSms(communicatorProfile, operation, fromPhoneNumber, message.ToPhoneNumber, (IDictionary<string, object>)options);

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
                return new VerificationCodeResult
                {
                    Status = ActionResultCode.Failed,
                    Message = e.Message,
                    Code = e.Code
                };
            }
            catch (Exception ex)
            {
                GetLog().Fatal(ex.Message, ex);
                return new VerificationCodeResult
                {
                    Status = ActionResultCode.Failed,
                    Message = ex.Message
                };
            }
        }
        
        #region private members
        private SmsMessageLog SendSms(
                CommunicatorProfile communicatorProfile,
                SmsOperation smsOperation, CommunicatorPhoneNumber fromPhoneNumber, string toPhoneNumber, IDictionary<string, object> messageArgs)
        {
            if (communicatorProfile == null) throw new ArgumentNullException("communicatorProfile");
            if (smsOperation == null) throw new ArgumentNullException("smsOperation");
            if (fromPhoneNumber == null) throw new ArgumentNullException("fromPhoneNumber");
            if (messageArgs == null) throw new ArgumentNullException("messageArgs");
            if (string.IsNullOrWhiteSpace(toPhoneNumber)) throw new ArgumentNullException("toPhoneNumber");
            
            //get the phone number that will receive this SMS message
            PhoneNumber toPhone;
            if (!PhoneNumber.TryParse(toPhoneNumber, out toPhone))
            {
                throw new ArgumentException(Messages.validation_InvalidPhoneNumber, "toPhoneNumber");
            }

            //get the associated communicator
            var communicatorProvider = _providersFactory.Create<ICommunicatorProvider> (communicatorProfile);

            var result = communicatorProvider.SendSms(smsOperation, fromPhoneNumber, toPhone, messageArgs);
            return CommunicationLogFactory<SmsMessageLog>.LogMessageResult(result, fromPhoneNumber, toPhone, smsOperation);
        }
        #endregion private members

        protected static TResult MissingSmsOperation<TResult>(string action)
             where TResult : ActionResult, new()
        {
            var actionResult = new TResult
            {
                Status = ActionResultCode.Failed,
                Message = string.Format(Messages.api_MissingSmsOperation,action)
            };
            GetLog().LogError(actionResult.Message);
            return actionResult;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_communicationLogRepository != null)
            {
                _communicationLogRepository.Dispose();
            }
            if (_smsOperationRepository != null)
            {
                _smsOperationRepository.Dispose();
            }
            if (_communicatorPhoneNumberRepository != null)
            {
                _communicatorPhoneNumberRepository.Dispose();
            }
        }
    }
}
