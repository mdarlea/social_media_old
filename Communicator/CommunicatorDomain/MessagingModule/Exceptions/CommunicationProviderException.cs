using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;

namespace Swaksoft.Domain.Communicator.MessagingModule.Exceptions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public class CommunicationProviderException : DomainServiceException
    {
        public CommunicationProviderException(string message) : base(message)
        {
        }

        public CommunicationLog CommunicationLog { get; set; }
    }
}
