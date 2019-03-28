using System;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;

namespace Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories
{
    public class StreamFilterRepository : Repository<StreamFilter>, IStreamFilterRepository
    {
        public StreamFilterRepository(ITransactionUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
