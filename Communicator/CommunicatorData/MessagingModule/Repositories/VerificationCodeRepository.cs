using Swaksoft.Domain.Seedwork;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.VerificationCodeAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.MessagingModule.Repositories
{
    public class VerificationCodeRepository : Repository<VerificationCode>
    {
        public VerificationCodeRepository(ITransactionUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
