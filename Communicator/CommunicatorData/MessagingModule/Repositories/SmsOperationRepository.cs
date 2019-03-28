using System.Data.Entity;
using System.Linq;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.MessagingModule.Repositories
{
    public class SmsOperationRepository : Repository<SmsOperation>
    {
        public SmsOperationRepository(ITransactionUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override SmsOperation Get(int id)
        {
            return id != 0 ? GetQuery().SingleOrDefault(e => e.Id == id) : null;
        }

        protected override IQueryable<SmsOperation> GetQuery()
        {
            return base.GetQuery().Include(vo => vo.Messages);

        }
    }
}
