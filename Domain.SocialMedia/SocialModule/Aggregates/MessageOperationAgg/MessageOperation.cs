using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageOperationAgg
{
    public abstract class MessageOperation : Entity
    {
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
    }
}
