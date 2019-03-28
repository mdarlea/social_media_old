using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg
{
    public class VoiceCallLog : CommunicationLog
    {
        public virtual short Attempt { get; set; }
        
        public void CallCompleted()
        {
            Attempt++;
        }
        public bool IsMaxAttempt()
        {
            //ToDo: revisit this cast
            var maxAttempts = ((VoiceOperation)MessageOperation).MaxAttempts;
            return (maxAttempts < 1 || Attempt < maxAttempts);
        }
    }
}
