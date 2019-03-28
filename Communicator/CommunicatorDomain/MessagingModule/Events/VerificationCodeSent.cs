using System;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.VerificationCodeAgg;
using Swaksoft.Domain.Seedwork.Events;

namespace Swaksoft.Domain.Communicator.MessagingModule.Events
{
    public class VerificationCodeSent : IDomainEvent 
    {
        private readonly CommunicationLog _communicationLog;

        public VerificationCodeSent(CommunicationLog communicationLog)
        {
            if (communicationLog == null) throw new ArgumentNullException("communicationLog");
            _communicationLog = communicationLog;
        }

        public long MemberNbr { get; set; }
        public VerificationCode VerificationCode { get; set; }

        public CommunicationLog CommunicationLog
        {
            get { return _communicationLog; }
        }
    }
}
