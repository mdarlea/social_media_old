using System.Collections.Generic;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageAgg
{
    /// <summary>
    /// Aggregate Root
    /// </summary>
    public class Message : Entity
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        
        public virtual string Text { get; set; }
        
        private HashSet<MessageOperation> _messageOperations;
        public virtual ICollection<MessageOperation> MessageOperations
        {
            get
            {
                return _messageOperations ?? (_messageOperations = new HashSet<MessageOperation>());
            }
            set
            {
                _messageOperations = new HashSet<MessageOperation>(value);
            }
        }
        
        public string ToString(params object[] args)
        {
            return string.Format(Text, args);
        }
    }
}
