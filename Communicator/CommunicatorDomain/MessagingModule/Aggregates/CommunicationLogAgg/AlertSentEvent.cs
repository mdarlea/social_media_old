using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg
{
    public abstract class AlertSentEvent : Entity
    {
        [Key, ForeignKey("CommunicationLog")]
        public override int Id  { get; protected set; }

        public virtual CommunicationLog CommunicationLog { get; set; }
    }
}
