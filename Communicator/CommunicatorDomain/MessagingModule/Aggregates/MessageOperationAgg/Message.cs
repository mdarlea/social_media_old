using System.Collections.Generic;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg
{
    public class Message : Entity
    {
        public virtual string Text { get; set; }
        public virtual ICollection<MessageOperation> MessageOperations { get; set; }

        public string ToString(params object[] args)
        {
            return string.Format(Text, args);
        }
    }
}
