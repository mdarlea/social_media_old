using Swaksoft.Domain.Seedwork;
using System.Collections.Generic;
using System.Text;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg
{
    /// <summary>
    /// Aggregate Root
    /// </summary>
    public abstract class MessageOperation : Entity
    {
        public virtual string Description { get; set; }
        
        public virtual string Action { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public string GetAllMessages(params object[] args)
        {
            var sb = new StringBuilder();
            foreach (var m in Messages)
            {
                sb.AppendLine(string.Format(m.Text,args));
            }
            return sb.ToString();
        }
    }
}
