using System.Data.Entity;
using System.Linq;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.MessagingModule.Repositories
{
    public class VoiceOperationRepository : Repository<VoiceOperation>
    {
        public VoiceOperationRepository(ITransactionUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override VoiceOperation Get(int id)
        {
            return id != 0 ? GetQuery().SingleOrDefault(e => e.Id == id) : null;
        }

        protected override IQueryable<VoiceOperation> GetQuery()
        {
            return base.GetQuery()
                .Include(vo => vo.SelectOptionMessage)
                .Include(vo => vo.InvalidOptionMessage)
                .Include(vo => vo.FromPhoneNumber)
                //.Include(vo => vo.Messages)
                //.Include(vo => vo.VoiceOptions.Select(o => o.NextVoiceOperation).Select(o => o.Messages))
                //.Include(vo => vo.VoiceOptions.Select(o => o.NextVoiceOperation).Select(o => o.VoiceOptions))
                .Include(vo => vo.SourceVoiceOptions);
        }
    }
}
