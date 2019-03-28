using System.Collections.Generic;
using System.Linq;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg
{
    /// <summary>
    /// Aggregate Root
    /// </summary>
    public class VoiceOperation : MessageOperation
    {
        public virtual int Timeout { get; set; }
        public virtual string FinishOnKey { get; set; }
        public virtual int NumDigits { get; set; }
        public virtual int MaxAttempts { get; set; }
        
        public virtual ICollection<VoiceOption> VoiceOptions { get; set; }
        public virtual ICollection<VoiceOption> SourceVoiceOptions { get; set; }
        public virtual int? InvalidOptionMessageId { get; set; }
        public virtual int? SelectOptionMessageId { get; set; }
        public virtual Message InvalidOptionMessage { get; set; }
        public virtual Message SelectOptionMessage { get; set; }

        public virtual int FromPhoneNumberId { get; set; }
        public virtual CommunicatorPhoneNumber FromPhoneNumber { get; set; }

        public List<string> FormatSelectOptionMessages()
        {
            var text = SelectOptionMessage.Text;
            return SelectOptionMessage == null ? null : VoiceOptions.Select(option => option.Format(text)).ToList();
        }

        public VoiceOption GetVoiceOption(int voiceOptionId)
        {
            var option =  (from o in SourceVoiceOptions where o.Id == voiceOptionId select o).SingleOrDefault();
            return option ?? (from o in VoiceOptions where o.Id == voiceOptionId select o).SingleOrDefault();
        }

        public List<string> GetInvalidMessage()
        {
            var messages = new List<string>();

            if (InvalidOptionMessage == null) return messages;

            messages.Add(InvalidOptionMessage.Text);
            if (SelectOptionMessage != null)
            {
                messages.AddRange(FormatSelectOptionMessages());
            }
            return messages;
        }

        public List<string> GetMessages()
        {
            var messages = new List<string>();

            messages.AddRange(Messages.Select(say => say.Text));
            if (messages.Any() && SelectOptionMessage != null)
            {
                messages.AddRange(FormatSelectOptionMessages());
            }
            return messages;
        }
    }
}
