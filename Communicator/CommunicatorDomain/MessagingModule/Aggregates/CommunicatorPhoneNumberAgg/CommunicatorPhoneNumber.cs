using System;
using System.Collections.Generic;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg
{
    public class CommunicatorPhoneNumber : Entity
    {
        public virtual PhoneNumber PhoneNumber { get; set; }
        public virtual DateTime? InactiveDate { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime EditDate { get; set; }

        public int ProfileId {get; set;}
        public CommunicatorProfile Profile { get; set; }

        public virtual ICollection<CommunicationLog> CommunicationLogs { get; set; }
    }
}
