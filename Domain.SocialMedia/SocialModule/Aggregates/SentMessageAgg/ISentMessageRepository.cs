using System;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg
{
    public interface ISentMessageRepository 
        : IRepository<SentMessage>
    {
    }
}
