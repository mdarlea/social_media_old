using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Contracts;

namespace Swaksoft.Domain.Communicator.MessagingModule.Services.Providers.Twilio
{
    public class VoiceOperationsService : IVoiceOperationsService
    {
        private readonly ITwimlServiceAgent _twimlServiceAgent;

        public VoiceOperationsService(ITwimlServiceAgent twimlServiceAgent)
        {
            if (twimlServiceAgent == null) throw new ArgumentNullException("twimlServiceAgent");
            _twimlServiceAgent = twimlServiceAgent;
        }

        public XElement SayMessage(CommunicatorProfile communicatorProfile, int? option, VoiceCallLog voiceCall)
        {
            if (communicatorProfile == null) throw new ArgumentNullException("communicatorProfile");
            if (voiceCall == null) throw new ArgumentNullException("voiceCall");

            //ToDo: revisit this cast
            //gets the voice operation
            var currentVoiceOperation = (VoiceOperation) voiceCall.MessageOperation;
            if (currentVoiceOperation == null)
            {
                throw new InvalidDataException(string.Format("Cannot find the voice operation associated with the {0} voice call id", voiceCall.Id));
            }

            //get the selected option
            var selectedOption = (option == null) ? null : currentVoiceOperation.GetVoiceOption((int)option);
            var nextVoiceOperation = (selectedOption == null) ? currentVoiceOperation : (selectedOption.NextVoiceOperation ?? currentVoiceOperation);

            var validOption = (option == null || selectedOption != null);
            var messages = (validOption)
                                ? nextVoiceOperation.GetMessages()
                                : nextVoiceOperation.GetInvalidMessage();

            var action = nextVoiceOperation.Action;
            var uri = (nextVoiceOperation.VoiceOptions.Any()) ? communicatorProfile.CreateUrl(new { action }) : null;
            
            var result = _twimlServiceAgent.SayMessage(
                uri, 
                nextVoiceOperation.Timeout, 
                nextVoiceOperation.FinishOnKey, 
                nextVoiceOperation.NumDigits, 
                messages.ToArray(), 
                null);

            //toDo: validation check
            if (validOption)
            {
                voiceCall.CallCompleted();
            }

            return result.XmlResponse;
        }

        public XElement SayVerificationCodeMessage(
            CommunicatorProfile communicatorProfile, 
            int? option,
            VoiceCallLog voiceCall)
        {
            if (communicatorProfile == null) throw new ArgumentNullException("communicatorProfile");
            if (voiceCall == null) throw new ArgumentNullException("voiceCall");

            var alert = voiceCall.AlertSentEvent as VerificationCodeSent;
            if (alert == null)
            {
                throw new InvalidDataException(string.Format("Could not find the associated verification code alert for VoiceDeliveredMessageId={0}", voiceCall.Id));
            }

            //ToDo: revisit this cast
            //gets the voice operation
            var currentVoiceOperation = (VoiceOperation) voiceCall.MessageOperation;
            if (currentVoiceOperation == null)
            {
                throw new InvalidDataException(string.Format("Cannot find the voice operation associated with the {0} voice call id", voiceCall.Id));
            }

            var verificationCode = (voiceCall.IsMaxAttempt()) ? alert.VerificationCode : null;
            var selectedOption = (option == null) ? null : currentVoiceOperation.GetVoiceOption((int)option);
            var nextVoiceOperation = (selectedOption == null) ? currentVoiceOperation : (selectedOption.NextVoiceOperation ?? currentVoiceOperation);

            //creates or gets the verification code
            var validOption = (option == null || selectedOption != null);
            var messages = (validOption)
                ? (verificationCode==null) ? new List<string>() : nextVoiceOperation.GetMessages()
                : nextVoiceOperation.GetInvalidMessage();

            var action = nextVoiceOperation.Action;
            var uri = (nextVoiceOperation.VoiceOptions.Any()) ? communicatorProfile.CreateUrl(new { action }) : null;

            var result = _twimlServiceAgent.SayMessage(
                uri, 
                nextVoiceOperation.Timeout, nextVoiceOperation.FinishOnKey, nextVoiceOperation.NumDigits, 
                messages.ToArray(), 
                (verificationCode==null) ? null : new Dictionary<string, object> 
                {
                    {"code",verificationCode.Code}
                });
            
            //toDo: validation check
            if (validOption && (verificationCode !=null))
            {
                voiceCall.CallCompleted();
            }
            return result.XmlResponse;
        }
    }
}
