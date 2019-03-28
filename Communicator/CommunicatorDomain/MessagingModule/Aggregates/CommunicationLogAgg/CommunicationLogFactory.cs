using System;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Contracts;
using Swaksoft.Domain.Communicator.MessagingModule.Exceptions;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg
{
    public static class CommunicationLogFactory<T>
        where T : CommunicationLog, new()
    {
        public static T CreateLog(
           string callId,
           CommunicatorPhoneNumber fromPhoneNumber,
           PhoneNumber toPhoneNumber,
           bool successfull,
           MessageOperation messageOperation)
        {
            if (fromPhoneNumber == null) throw new ArgumentNullException("fromPhoneNumber");
            if (toPhoneNumber == null) throw new ArgumentNullException("toPhoneNumber");
            if (messageOperation == null) throw new ArgumentNullException("messageOperation");
            if (string.IsNullOrWhiteSpace(callId)) throw new ArgumentNullException("callId");

            var deliveredMessage = new T
            {
                CallId = callId
            };

            deliveredMessage.SetFromPhoneNumber(fromPhoneNumber);
            deliveredMessage.SetMessageOperation(messageOperation);
            deliveredMessage.ToPhoneNumber = toPhoneNumber;
            deliveredMessage.Successful = successfull;
            deliveredMessage.LogDate = DateTime.UtcNow;

            return deliveredMessage;
        }

        public static T LogMessageResult(
            MessageResult result,
            CommunicatorPhoneNumber fromPhoneNumber, 
            PhoneNumber toPhone,
            MessageOperation messageOperation)
        {
            var log = CreateLog(
                result.Sid,
                fromPhoneNumber, toPhone,
                result.Status != ActionResultCode.Success, messageOperation);

            if (result.Status != ActionResultCode.Success)
            {
                throw new CommunicationProviderException(result.Message)
                {
                    Code = result.Code,
                    MoreInfo = result.MoreInfo,
                    CommunicationLog = log
                };
            }
            return log;
        }
    }
}
