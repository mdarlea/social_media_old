using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg
{
    public interface ICommunicatorPhoneNumberRepository : IRepository<CommunicatorPhoneNumber>
    {
        CommunicatorPhoneNumber GetSms();
    }
}
