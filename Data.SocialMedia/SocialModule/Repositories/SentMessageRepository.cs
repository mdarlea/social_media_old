using System;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;

namespace Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories
{
    public class SentMessageRepository 
        :Repository<SentMessage>,ISentMessageRepository
    {
        public SentMessageRepository(ITransactionUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
