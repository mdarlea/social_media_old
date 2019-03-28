using System.Collections.Generic;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg
{
    public class VoiceOption : Entity
    {
        public virtual string Key { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<VoiceOperation> VoiceOperations { get; set; }

        public virtual VoiceOperation NextVoiceOperation { get; set; }
        public virtual int? NextVoiceOperationId { get; set; }

        public string Format(string template)
        {
            return string.Format(template, Key, Description);
        }
    }
}
