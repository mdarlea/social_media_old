using Swaksoft.Domain.Seedwork;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.MessagingModule.Repositories
{
    public class CommunicatorPhoneNumberRepository : Repository<CommunicatorPhoneNumber>, ICommunicatorPhoneNumberRepository
    {
        public CommunicatorPhoneNumberRepository(ITransactionUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CommunicatorPhoneNumber GetSms()
        {
            throw new System.NotImplementedException();
        }
    }
}
