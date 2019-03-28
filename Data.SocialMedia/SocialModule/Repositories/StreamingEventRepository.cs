using System;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamingEventAgg;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;

namespace Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories
{
    public class StreamingEventRepository : Repository<StreamingEvent>, IStreamingEventRepository
    {
        public StreamingEventRepository(ITransactionUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
