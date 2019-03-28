using Swaksoft.Domain.Seedwork;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;
using System.Data.Entity;
using System.Linq;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.MessagingModule.Repositories
{
    public class CommunicatorProfileRepository : Repository<CommunicatorProfile>
    {
        public CommunicatorProfileRepository(ITransactionUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override CommunicatorProfile Get(int id)
        {
            return id != 0 ? GetQuery().SingleOrDefault(e => e.Id == id) : null;
        }

        protected override IQueryable<CommunicatorProfile> GetQuery()
        {
            return base.GetQuery().Include(p => p.AllowNumbers);

        }
    }
}
