using System;
using System.Collections.Generic;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Contracts;
using Swaksoft.Domain.Seedwork.Aggregates.ProfileAgg;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;

namespace Swaksoft.Domain.Communicator.MessagingModule.Providers
{
    public interface ICommunicatorProvider : IProvider
    {
        MessageResult InitiateOutboundCall(
            VoiceOperation voiceOperation,
            CommunicatorPhoneNumber @from,
            PhoneNumber to,
            IDictionary<string, object> messageArgs);

        MessageResult SendSms(
            SmsOperation smsOperation,
            CommunicatorPhoneNumber @from,
            PhoneNumber to,
            IDictionary<string, object> messageArgs);
    }

    public abstract class CommunicatorProvider<T> : ICommunicatorProvider
          where T : CommunicatorProfile
    {
        protected CommunicatorProvider(T communicatorProfile)
        {
            if (communicatorProfile == null) throw new ArgumentNullException("communicatorProfile");
            CommunicatorProfile = communicatorProfile;
        }

        protected T CommunicatorProfile { get; private set; }

        public abstract MessageResult InitiateOutboundCall(VoiceOperation voiceOperation, CommunicatorPhoneNumber @from, PhoneNumber to,
            IDictionary<string, object> messageArgs);

        public abstract MessageResult SendSms(SmsOperation smsOperation, CommunicatorPhoneNumber @from, PhoneNumber to, IDictionary<string, object> messageArgs);

        public Profile Profile
        {
            get
            {
                return CommunicatorProfile;
            }
        }
    }
}
