using System;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg
{
    public abstract class CommunicationLog : Entity
    {
        public virtual string CallId { get; set; }
        public virtual AlertSentEvent AlertSentEvent { get; set; }
        public virtual int MessageOperationId { get; set; }
        public virtual MessageOperation MessageOperation { get; set; }
        public virtual bool Successful { get; set; }
        public virtual DateTime LogDate { get; set; }
        public virtual PhoneNumber ToPhoneNumber { get; set; }

        public virtual int FromPhoneNumberId { get; set; }
        public virtual CommunicatorPhoneNumber FromPhoneNumber { get; set; }

        public void SetFromPhoneNumber(CommunicatorPhoneNumber fromPhoneNumber)
        {
            if (fromPhoneNumber == null) throw new ArgumentNullException("fromPhoneNumber");

            FromPhoneNumber = fromPhoneNumber;
            FromPhoneNumberId = fromPhoneNumber.Id;
        }

        //public virtual long MemberNbr { get; set; }

        public T ChangeAlertSentEvent<T>()
             where T:AlertSentEvent, new()
        {
            var alertSent = new T
            {                
                CommunicationLog = this
            };
			alertSent.ChangeCurrentIdentity(Id);
            AlertSentEvent = alertSent;
            return alertSent;
        }

        public virtual void SetMessageOperation(MessageOperation messageOperation)
        {
            MessageOperation = messageOperation;
            MessageOperationId = messageOperation.Id;
        }
    }
}
