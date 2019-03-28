using Swaksoft.Domain.Seedwork;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.MessagingModule.Repositories
{
    public class CommunicationLogRepository : Repository<CommunicationLog>
    {
        protected CommunicationLogRepository(ITransactionUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override CommunicationLog GetSingle(Expression<Func<CommunicationLog, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException("filter");
            
            return GetSet().SingleOrDefault(filter);
        }
    }
}
