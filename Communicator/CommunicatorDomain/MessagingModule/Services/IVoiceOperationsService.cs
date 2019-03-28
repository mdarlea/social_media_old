using System.Xml.Linq;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;

namespace Swaksoft.Domain.Communicator.MessagingModule.Services
{
    public interface IVoiceOperationsService
    {
        XElement SayMessage(CommunicatorProfile communicatorProfile, int? option, VoiceCallLog voiceCall);

        XElement SayVerificationCodeMessage(CommunicatorProfile communicatorProfile, int? option, VoiceCallLog voiceCall);
    }
}
