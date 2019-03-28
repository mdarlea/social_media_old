using System;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageAgg;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;

namespace Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ITransactionUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
