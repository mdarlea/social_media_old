using System;
using System.Collections.Generic;
using System.Linq;
using AntiCorruption.Twillio.Extensions;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Providers;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Twilio;
using MessageResult = Swaksoft.Domain.Communicator.MessagingModule.Contracts.MessageResult;

namespace AntiCorruption.Twillio
{
    public class TwillioCommunicatorProvider : CommunicatorProvider<TwilioProfile>
    {
        public override MessageResult InitiateOutboundCall(
            VoiceOperation voiceOperation, 
            CommunicatorPhoneNumber @from, PhoneNumber to,
            IDictionary<string, object> messageArgs)
        {
            if (voiceOperation == null) throw new ArgumentNullException("voiceOperation");
            if (@from == null) throw new ArgumentNullException("from");
            if (to == null) throw new ArgumentNullException("to");

            var uri = CommunicatorProfile.CreateUrl(new { action = voiceOperation.Action });

            var result = InitiateOutboundCall(
                CommunicatorProfile.AuthorizationToken,
                uri,
                CommunicatorProfile.CallTimeout.Seconds,
                @from.PhoneNumber.ToString(),
                to.ToString());

            return result.ToProviderActionResult<MessageResult>(r =>
            {
                r.Sid = result.Sid;
            });
        }

        public override MessageResult SendSms(
             SmsOperation smsOperation, 
             CommunicatorPhoneNumber @from, PhoneNumber to, 
             IDictionary<string, object> messageArgs)
        {
            if (smsOperation == null) throw new ArgumentNullException("smsOperation");
            if (@from == null) throw new ArgumentNullException("from");
            if (to == null) throw new ArgumentNullException("to");
            if (messageArgs == null) throw new ArgumentNullException("messageArgs");

            var fromPhoneNumber = string.Format("{0:twilio}", @from.PhoneNumber);
            var toPhoneNumber = string.Format("{0:twilio}", to);

            var body = smsOperation.GetAllMessages(messageArgs.Select(p => p.Value));
            var result = SendSms(CommunicatorProfile.AuthorizationToken, fromPhoneNumber, toPhoneNumber, body);

            return result.ToProviderActionResult<MessageResult>(r =>
            {
                r.Sid = result.Sid;
            });
        }

        private static Call InitiateOutboundCall(TwilioAuthToken authToken, Url uri, int timeout, string from, string toPhoneNumber)
        {
            if (authToken == null) throw new ArgumentNullException("authToken");
            if (uri == null) throw new ArgumentNullException("uri");
            if (@from == null) throw new ArgumentNullException("from");
            if (toPhoneNumber == null) throw new ArgumentNullException("toPhoneNumber");

            var callOpts = new CallOptions
            {
                From = @from,
                To = toPhoneNumber,
                //Timeout = timeout,
                //IfMachine = "Hangup",
                Url = uri.ToString()
            };

            var client = new TwilioRestClient(authToken.AccountSid, authToken.AuthToken);
            return client.InitiateOutboundCall(callOpts);
        }

        private static SMSMessage SendSms(TwilioAuthToken authToken, string from, string to, string body)
        {
            if (authToken == null) throw new ArgumentNullException("authToken");

            var client = new TwilioRestClient(authToken.AccountSid, authToken.AuthToken);
            return client.SendSmsMessage(from, to, body);
        }

        public TwillioCommunicatorProvider(TwilioProfile communicatorProfile) : base(communicatorProfile)
        {
        }
    }
}
